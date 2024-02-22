using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestTarget
{
    ENEMY,
    NATURE
}

public class QuestManager : MonoBehaviour
{
    public QuestSO QuestSO; // 퀘스트 데이터를 저장하고 있는 ScriptableObject
   
    public PlayerQuestList playerQuestList;

    [SerializeField] private int _questCount = 4;
    [SerializeField] private int _mainQuestCount = 4;
    private int _halfLength;
    private int _currentPlayerGold = 0;

    public List<Quest> ActiveMainQuests = new List<Quest>(); // 메인 퀘스트 목록
    public List<Quest> ActiveDailyQuests = new List<Quest>(); // 퀘스트 목록
    public List<Quest> AcceptQuestList = new List<Quest>(); // 수락한 퀘스트 목록

    public delegate void QuestAcceptedEvent(List<Quest> acceptedQuests, int quantity);
    public event QuestAcceptedEvent OnQuestAccepted;

    public event Action<int, int> OnQuestValueUpdate;

    /// <summary>
    /// Key: 퀘스트 번호, Value: 수량
    /// </summary>
    public Dictionary<int, int> EnemyQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    public Dictionary<int, int> NatureQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    public Dictionary<int, int> MainQuestQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할

    public Dictionary<int, int> loadActivQuestDic;

    private List<int> _questKey;

    public List<int> LoadProgressQuantities;
    public List<int> LoadAcceptQuestQuantities;

    private GameStateManager _gameStateManager;


    private void Start()
    {
        GameManager.Instance.GlobalTimeManager.OnInitQuest += InitializeDailyQuest;
        GameManager.Instance.GlobalTimeManager.OnInitQuest += InitializeMainQuest;
        //EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
        //ItemObject.OnQuestTargetInteraction += UpdateNatureQuestProgress;
    }

    private void OnEnable()
    {
        _gameStateManager  = GameManager.Instance.GameStateManager;

        if(_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            Dictionary<int, int> enemyQuestNumber = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveEnemyQuestProgress;
            Dictionary<int, int> natureQuestNumber = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveNatureQuestProgress;

            Dictionary<int, int> loadActiveQuestDic = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveActiveQuest;
            
           _questKey = enemyQuestNumber.Keys.Concat(natureQuestNumber.Keys).ToList();
            LoadProgressQuantities = enemyQuestNumber.Values.Concat(natureQuestNumber.Values).ToList();

            List<int> activeQuestKey = loadActiveQuestDic.Keys.ToList();
            List<int> activeQuestValue = loadActiveQuestDic.Values.ToList();

            _halfLength = _questKey.Count / 2;

            for(int i = 0; i < _questKey.Count; i++)
            {
                if( i < _halfLength)
                {
                    EnemyDailyQuest enemyDailyQuest = new EnemyDailyQuest(QuestSO, _questKey[i]);
                    enemyDailyQuest.TargetID = activeQuestKey[i];
                    enemyDailyQuest.TargetQuantity = activeQuestValue[i];
                    ActiveDailyQuests.Add(enemyDailyQuest);
                }
                else
                {
                    // 나머지 반은 NatureDailyQuest 생성
                    NatureDailyQuest natureDailyQuest = new NatureDailyQuest(QuestSO, _questKey[i]);
                    natureDailyQuest.TargetID = activeQuestKey[i];
                    natureDailyQuest.TargetQuantity = activeQuestValue[i];
                    ActiveDailyQuests.Add(natureDailyQuest);
                }
            }

            List<int> loadAcceptQuest = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveAcceptQuestID;
            LoadAcceptQuestQuantities = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveAcceptQuest.Values.ToList();

            EnemyQuantityDict = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveEnemyQuestProgress;
            NatureQuantityDict = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveNatureQuestProgress;

            for (int i = 0; i < loadAcceptQuest.Count; i++)
            {
                Quest quest = ActiveDailyQuests[i];
                AcceptQuest(quest);
            }

        }
    }

    // 퀘스트를 초기화하고 추가하는 메서드
    public void InitializeDailyQuest()
    {

        _gameStateManager.CurrentGameState = GameState.NEWGAME;
        ActiveDailyQuests.Clear();
        AcceptQuestList.Clear();
        EnemyQuantityDict.Clear();
        NatureQuantityDict.Clear();

        int enemyQuestID = 10001;
        int natureQuestID = 20001;

        int currentQuantity = 0;

        for (int i = 0; i < _questCount; i++) 
        { 
            if(i < _questCount / 2)
            {
                // Do While로 처리한 이유: 퀘스트 내용이 중복되지 않게 하기 위함
                EnemyDailyQuest newEnemyQuest;
                do
                {
                    newEnemyQuest = new EnemyDailyQuest(QuestSO, enemyQuestID);
                } while (CheckForDuplicateTargetID(newEnemyQuest.TargetID));

                ActiveDailyQuests.Add(newEnemyQuest);
                EnemyQuantityDict.Add(enemyQuestID, currentQuantity);
                Debug.Log("퀘스트 " + enemyQuestID + " 내용: " + newEnemyQuest.GetQuestDescription());
                enemyQuestID++;
            }
            else // 나머지 절반은 Nature 퀘스트에 할당
            {
                //NatureDailyQuest newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, natureCount);
                NatureDailyQuest newNatureQuest;
                do
                {
                    newNatureQuest = new NatureDailyQuest(QuestSO, natureQuestID);
                } while (CheckForDuplicateTargetID(newNatureQuest.TargetID));

                ActiveDailyQuests.Add(newNatureQuest);
                NatureQuantityDict.Add(natureQuestID, currentQuantity);
                Debug.Log("퀘스트 " + natureQuestID + " 내용: " + newNatureQuest.GetQuestDescription());
                natureQuestID++;
            }

        }
    }

    // 글타매에 1일차에 이벤트 등록해서 이걸 구독해놨어요 1일차가 되면 바로 이 함수가 한번 실행이됩니다.
    // 1일차에 이벤트가 등록되면서 이 함수가 실행됨 - 실행되면서 함수 호출되고 동시에 퀘스트 4개 짜라락 구독하고
    // 퀘스트가 수행될 때 이벤트가 호출되면서 수치 조정하고 완료되면 퀘스트 완료

    // 문제 구독을 못함 tycoonManager가 tycoon에 있어서
    private void InitializeMainQuest()
    {
        int currentQuantity = 0;

        for(int i = 0; i < QuestSO.MainQuestData.Length; i++)
        {
            int mainQuestID = QuestSO.MainQuestData[i].QuestID;
            MainQuest mainQuest = new MainQuest(QuestSO, mainQuestID, i);
            ActiveMainQuests.Add(mainQuest);
            MainQuestQuantityDict.Add(mainQuestID, currentQuantity);
        }

        //TODO 퀘스트의 각 이벤트 걸어줌
        // 타이쿤 - 타이쿤 매니저 angry어쩌구
        // 타이쿤 결과창이 떴을 때 Angry가 0명이다 -> 퀘 완

        GoDungeon goDungeon = GameManager.Instance.UIManager.PopupDic[UIName.GoDungeonUI].GetComponent<GoDungeon>();
        Player player = GameManager.Instance.Player.GetComponent<Player>();

        GameManager.Instance.DataManager.OnTycoonMainQuest += UpdateMainQuest; // 타이쿤 구독
        EnemyHealthSystem.OnMainQuestTargetDie += UpdateMainQuest; // 불닭 구독
        goDungeon.OnEnterDungeon += UpdateMainQuest;
        player.Data.PlayerData.OnAccumulateGold += UpdateMainQuest;

    }

    // 중복확인
    private bool CheckForDuplicateTargetID(int targetID)
    {
        foreach (var quest in ActiveDailyQuests)
        {
            if (quest.GetTargetID() == targetID)
            {
                return true;
            }
        }
        return false;
    }

    // TODO 퀘스트를 수락하고 처리하는 메서드
    public void AcceptQuest(Quest quest)
    {
        if (!AcceptQuestList.Contains(quest))
        {
            // 이 리스트를 BaseUI 에 전달
            AcceptQuestList.Add(quest);

            if (OnQuestAccepted != null)
            {
                OnQuestAccepted.Invoke(AcceptQuestList, 0);
            }
            else
            {
                for(int i = 0; i < ActiveDailyQuests.Count; i++)
                {
                    if(i < _halfLength)
                    {
                        playerQuestList.PlayerAceeptQuestList(AcceptQuestList, LoadProgressQuantities[i]);

                    }
                    else
                    {
                        playerQuestList.PlayerAceeptQuestList(AcceptQuestList, LoadProgressQuantities[i]);
                    }
             
                }
            }

            if (quest is EnemyDailyQuest)
            {
                EnemyHealthSystem.OnQuestTargetDie -= UpdateEnemyQuestProgress;
                EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
            }
            else if (quest is NatureDailyQuest)
            {
                ItemObject.OnQuestTargetInteraction -= UpdateNatureQuestProgress;
                ItemObject.OnQuestTargetInteraction += UpdateNatureQuestProgress;
            }
        }
    }


    // TODO 퀘스트 진행 관리(수락했을 때)
    public void UpdateEnemyQuestProgress(int targetID)
    {
        foreach(Quest quest in AcceptQuestList)
        {
            if (quest is EnemyDailyQuest)
            {
                EnemyDailyQuest enemyQuest = (EnemyDailyQuest)quest;
                if (enemyQuest.TargetID == targetID)
                {
                    // 퀘스트 번호 
                    int questNumber = enemyQuest.QuestNumber;
                    if (EnemyQuantityDict.ContainsKey(questNumber))
                    {
                        EnemyQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(EnemyQuantityDict[questNumber], targetID);

                        Debug.Log("수량 줄어줘:  " + EnemyQuantityDict[questNumber]);
                        if (EnemyQuantityDict[questNumber] >= enemyQuest.TargetQuantity)
                        {
                            // 퀘스트 삭제
                            CompleteQuest(quest);
                        }
                    }
                    break;
                }
            }
        }
    }

    public void UpdateNatureQuestProgress(int targetID) 
    {
        foreach (Quest quest in AcceptQuestList)
        {
            if (quest is NatureDailyQuest)
            {
                NatureDailyQuest natureItemQuest = (NatureDailyQuest)quest;
                if (natureItemQuest.TargetID == targetID)
                {
                    // 퀘스트 번호 
                    int questNumber = natureItemQuest.QuestNumber;
                    if (NatureQuantityDict.ContainsKey(questNumber))
                    {
                        NatureQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(NatureQuantityDict[questNumber], targetID);

                        Debug.Log("수량 줄어줘:  " + NatureQuantityDict[questNumber]);
                        if (NatureQuantityDict[questNumber] >= natureItemQuest.TargetQuantity)
                        {
                            // 퀘스트 삭제
                            CompleteQuest(quest);
                        }
                    }
                    break;
                }
            }
        }
    }

    public void UpdateMainQuest(int questID)
    {
        int accumulate  = GameManager.Instance.DataManager.QuestDataList.MainQuestData[3].QuestID;
        for(int i = 0; i < ActiveMainQuests.Count; i++)
        {

            int mainQuestNumber = ActiveMainQuests[i].QuestNumber;
            if (MainQuestQuantityDict.ContainsKey(mainQuestNumber) && mainQuestNumber == questID)
            {
                Debug.Log("수량 체크");

                MainQuestQuantityDict[mainQuestNumber]++;

                if(questID == accumulate)
                {
                    Player player = GameManager.Instance.Player.GetComponent<Player>();
                    int playerGold = player.Data.PlayerData.PlayerGold;

                    if(_currentPlayerGold < playerGold)
                    {
                        MainQuestQuantityDict[mainQuestNumber] += playerGold - _currentPlayerGold;
                    }
                    
                    _currentPlayerGold = playerGold;

                    MainQuestQuantityDict[mainQuestNumber]--;
                    Debug.Log("???" + MainQuestQuantityDict[mainQuestNumber]);
                }
                // 불닭 잡았다 그럼 이 함수가 이벤트로인해 호출되고 수량 ++
                // 1 -> 완료
                // 던전 들어갔다 그러면 들어갔을 때 이벤트 호출 ++
                // 1 -> 완료

                // 누적돈 setgolod할때 이벤트 호출 수량 ++
                // 50000 -> 완료

                if (MainQuestQuantityDict[mainQuestNumber] >= ActiveMainQuests[i].TargetQuantity)
                {
                    // 퀘스트 삭제
                    CompleteQuest(ActiveMainQuests[i]);
                }
                break;
            }
        }
    }

    //TODO 퀘스트를 완료하고 보상을 처리
    public void CompleteQuest(Quest quest)
    {
        // 퀘스트 완료시 보상 등등
        QuestReward(quest);

        if(quest is MainQuest)
        {
            ActiveMainQuests.Remove(quest);
            Debug.Log("-완-");
            return;
        }
        AcceptQuestList.Remove(quest);
        Debug.Log("퀘스트 완");


        // 이벤트 구독 해제
        if (AcceptQuestList.Count == 0)
        {
            if (quest is EnemyDailyQuest)
            {
                EnemyHealthSystem.OnQuestTargetDie -= UpdateEnemyQuestProgress;
            }
            else if (quest is NatureDailyQuest)
            {
                ItemObject.OnQuestTargetInteraction -= UpdateNatureQuestProgress;
            }
        }

        foreach (Quest newquest in AcceptQuestList)
        {
            Debug.Log(newquest.QuestNumber);
        }

        GameManager.Instance.EffectManager.QuestCompleteEffect();
    }

    // 골드 보상 또는 경험치보상
    // TODO 골드보상 - 채집물가격 / 2
    // 경험치 - 플레이어 레벨 * 500
    private void QuestReward(Quest quest)
    {
        Player player = GameManager.Instance.Player.GetComponent<Player>();

        if (quest is EnemyDailyQuest)
        {
            player.PlayerExpSystem.GetExpPlus(quest.EnemyQuestReward);
        }
        else if(quest is NatureDailyQuest)
        {
            int rewardGold = player.Data.PlayerData.PlayerGold + quest.NatureQuestReward;
            player.Data.PlayerData.PlayerGold = rewardGold;
        }
    }



}
