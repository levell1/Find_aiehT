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

            //Quest newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, i);
            //Quest newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, i);
            //ActiveQuests.Add(newEnemyQuest);
            //ActiveQuests.Add(newNatureQuest);

            //_enemyQuantityDict.Add(i, newEnemyQuest.TargetQuantity);
            //_natureQuantityDict.Add(i, newNatureQuest.TargetQuantity);

            //Debug.Log(_enemyQuantityDict[i]);

            //Debug.Log("퀘스트 " + (i + 1) + " 내용: " + newEnemyQuest.GetQuestDescription());
            //Debug.Log("퀘스트 " + (i + 1) + " 내용: " + newNatureQuest.GetQuestDescription());
        }
        //Quest newQuest = new Quest(QuestSO);
        //activeQuests.Add(newQuest);
        //activeQuests.Add(newQuest2);
    }

    private void InitializeMainQuest()
    {
        int mainQuestID = 30001;
        for(int i = 0; i < QuestSO.MainQuestData.Length; i++)
        {
            MainQuest mainQuest = new MainQuest(QuestSO, mainQuestID, i);
            ActiveMainQuests.Add(mainQuest);
            mainQuestID++;
        }

        Debug.Log(ActiveMainQuests.Count);

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

    //TODO 퀘스트를 완료하고 보상을 처리
    public void CompleteQuest(Quest quest)
    {
        // 퀘스트 완료시 보상 등등
        QuestReward(quest);
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
