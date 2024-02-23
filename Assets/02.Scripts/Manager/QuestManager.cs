using System;
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
    public QuestSO QuestSO; 
   
    public PlayerQuestList playerQuestList;

    [SerializeField] private int _questCount = 4;
    [SerializeField] private int _mainQuestCount = 4;
    private int _halfLength;
    private int _currentPlayerGold = 0;

    public List<Quest> ActiveMainQuests = new List<Quest>(); 
    public List<Quest> ActiveDailyQuests = new List<Quest>(); 
    public List<Quest> AcceptQuestList = new List<Quest>(); 

    public delegate void QuestAcceptedEvent(List<Quest> acceptedQuests, int quantity);
    public event QuestAcceptedEvent OnQuestAccepted;

    public event Action<int, int> OnQuestValueUpdate;

    /// <summary>
    /// Key: 퀘스트 번호, Value: 수량
    /// </summary>
    public Dictionary<int, int> EnemyQuantityDict = new Dictionary<int, int>(); 
    public Dictionary<int, int> NatureQuantityDict = new Dictionary<int, int>(); 
    public Dictionary<int, int> MainQuestQuantityDict = new Dictionary<int, int>();

    public Dictionary<int, int> loadActivQuestDic;

    private List<int> _questKey;

    public List<int> LoadProgressQuantities;
    public List<int> LoadAcceptQuestQuantities;

    private GameStateManager _gameStateManager;
    private Player _player;
    private SavePlayerData _savePlayerData;

    private void Start()
    {
        GameManager.Instance.GlobalTimeManager.OnInitQuest += InitializeDailyQuest;
        if(_gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            GameManager.Instance.GlobalTimeManager.OnInitMainQuest += InitializeMainQuest;
        }
    }

    private void OnEnable()
    {
        _gameStateManager  = GameManager.Instance.GameStateManager;
        _player = GameManager.Instance.Player.GetComponent<Player>();
        _savePlayerData = GameManager.Instance.JsonReaderManager.LoadedPlayerData;
        LoadDailyQuest();
        LoadMainQuest();
    }

    private void LoadDailyQuest()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            Dictionary<int, int> enemyQuestNumber = _savePlayerData.SaveEnemyQuestProgress;
            Dictionary<int, int> natureQuestNumber = _savePlayerData.SaveNatureQuestProgress;

            Dictionary<int, int> loadActiveQuestDic = _savePlayerData.SaveActiveQuest;
            Dictionary<int, int> loadEnemyQuestRewardDic = _savePlayerData.SaveEnemyQuestReward;
            Dictionary<int, int> loadNautreQuestRewardDic = _savePlayerData.SaveNatureQuestReward;

            _questKey = enemyQuestNumber.Keys.Concat(natureQuestNumber.Keys).ToList();
            LoadProgressQuantities = enemyQuestNumber.Values.Concat(natureQuestNumber.Values).ToList();

            List<int> activeQuestKey = loadActiveQuestDic.Keys.ToList();
            List<int> activeQuestValue = loadActiveQuestDic.Values.ToList();

            _halfLength = _questKey.Count / 2;

            for (int i = 0; i < _questKey.Count; i++)
            {
                if (i < _halfLength)
                {
                    EnemyDailyQuest enemyDailyQuest = new EnemyDailyQuest(QuestSO, _questKey[i]);
                    enemyDailyQuest.TargetID = activeQuestKey[i];
                    enemyDailyQuest.TargetQuantity = activeQuestValue[i];
                    enemyDailyQuest.EnemyTotalQuestReward = loadEnemyQuestRewardDic[_questKey[i]];
                    ActiveDailyQuests.Add(enemyDailyQuest);
                }
                else
                {
                    NatureDailyQuest natureDailyQuest = new NatureDailyQuest(QuestSO, _questKey[i]);
                    natureDailyQuest.TargetID = activeQuestKey[i];
                    natureDailyQuest.TargetQuantity = activeQuestValue[i];
                    natureDailyQuest.NatureToalQuestReward = loadNautreQuestRewardDic[_questKey[i]];
                    ActiveDailyQuests.Add(natureDailyQuest);
                }
            }

            List<int> loadAcceptQuest = _savePlayerData.SaveAcceptQuestID;
            LoadAcceptQuestQuantities = _savePlayerData.SaveAcceptQuest.Values.ToList();

            EnemyQuantityDict = _savePlayerData.SaveEnemyQuestProgress;
            NatureQuantityDict = _savePlayerData.SaveNatureQuestProgress;

            for (int i = 0; i < loadAcceptQuest.Count; i++)
            {
                Quest quest = ActiveDailyQuests[i];
                AcceptQuest(quest);
            }

        }
    }

    private void LoadMainQuest()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            InitializeMainQuest();
            MainQuestQuantityDict = _savePlayerData.SaveActiveMainQuest;
            
            Dictionary<int ,bool> LoadQuestProgress = _savePlayerData.SaveActiveMainQuestProgress;

            foreach (Quest quest in ActiveMainQuests)
            {
                bool loadQuestProgress = LoadQuestProgress[quest.QuestNumber];
                quest.IsProgress = loadQuestProgress;
            }
        }
    }


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
                EnemyDailyQuest newEnemyQuest;
                do
                {
                    newEnemyQuest = new EnemyDailyQuest(QuestSO, enemyQuestID);
                } while (CheckForDuplicateTargetID(newEnemyQuest.TargetID));

                ActiveDailyQuests.Add(newEnemyQuest);
                EnemyQuantityDict.Add(enemyQuestID, currentQuantity);
                enemyQuestID++;
            }
            else 
            {
                
                NatureDailyQuest newNatureQuest;
                do
                {
                    newNatureQuest = new NatureDailyQuest(QuestSO, natureQuestID);
                } while (CheckForDuplicateTargetID(newNatureQuest.TargetID));

                ActiveDailyQuests.Add(newNatureQuest);
                NatureQuantityDict.Add(natureQuestID, currentQuantity);
                natureQuestID++;
            }

        }
    }

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

        int goldQuestNum = 30004;

        if (MainQuestQuantityDict.ContainsKey(goldQuestNum) && _gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            _currentPlayerGold = MainQuestQuantityDict[goldQuestNum];
        }

        GoDungeon _goDungeon = GameManager.Instance.UIManager.PopupDic[UIName.GoDungeonUI].GetComponent<GoDungeon>();

        GameManager.Instance.DataManager.OnTycoonMainQuest += UpdateMainQuest; 
        EnemyHealthSystem.OnMainQuestTargetDie += UpdateMainQuest; 
        _goDungeon.OnEnterDungeon += UpdateMainQuest;
        _player.Data.PlayerData.OnAccumulateGold += UpdateMainQuest;

    }

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

    public void AcceptQuest(Quest quest)
    {
        if (!AcceptQuestList.Contains(quest))
        {
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


    public void UpdateEnemyQuestProgress(int targetID)
    {
        foreach(Quest quest in AcceptQuestList)
        {
            if (quest is EnemyDailyQuest)
            {
                EnemyDailyQuest enemyQuest = (EnemyDailyQuest)quest;
                if (enemyQuest.TargetID == targetID)
                {
                    int questNumber = enemyQuest.QuestNumber;
                    if (EnemyQuantityDict.ContainsKey(questNumber))
                    {
                        EnemyQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(EnemyQuantityDict[questNumber], targetID);

                        if (EnemyQuantityDict[questNumber] >= enemyQuest.TargetQuantity)
                        {
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
                    int questNumber = natureItemQuest.QuestNumber;
                    if (NatureQuantityDict.ContainsKey(questNumber))
                    {
                        NatureQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(NatureQuantityDict[questNumber], targetID);

                        if (NatureQuantityDict[questNumber] >= natureItemQuest.TargetQuantity)
                        {
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
                }

                if (MainQuestQuantityDict[mainQuestNumber] >= ActiveMainQuests[i].TargetQuantity)
                {
                    CompleteQuest(ActiveMainQuests[i], questID);
                }
                break;
            }
        }
    }

    public void CompleteQuest(Quest quest, int questID)
    {  
        if (quest is MainQuest)
        {
            quest.IsProgress = true;

            if (questID == 30001)
            {
                GameManager.Instance.DataManager.OnTycoonMainQuest -= UpdateMainQuest; 
            }
            else if(questID == 30002)
            {
                GoDungeon _goDungeon = GameManager.Instance.UIManager.PopupDic[UIName.GoDungeonUI].GetComponent<GoDungeon>();
                _goDungeon.OnEnterDungeon -= UpdateMainQuest;
            }
            else if (questID == 30003)
            {
                EnemyHealthSystem.OnMainQuestTargetDie -= UpdateMainQuest; 
            }
            else if (questID == 30004)
            {
                _player.Data.PlayerData.OnAccumulateGold -= UpdateMainQuest;
            }
        }
        QuestReward(quest);
    }


    public void CompleteQuest(Quest quest)
    {
        QuestReward(quest);

        AcceptQuestList.Remove(quest);

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

        GameManager.Instance.EffectManager.QuestCompleteEffect();
    }

    private void QuestReward(Quest quest)
    {
        Player player = GameManager.Instance.Player.GetComponent<Player>();

        if (quest is EnemyDailyQuest)
        {
            player.PlayerExpSystem.GetExpPlus(quest.EnemyQuestReward);
        }
        else if(quest is NatureDailyQuest)
        {
            int rewardGold = player.Data.PlayerData.PlayerGold + quest.NatureItemPrice;
            player.Data.PlayerData.PlayerGold += rewardGold;
        }
        else if(quest is MainQuest)
        {
            player.Data.PlayerData.PlayerGold += quest.MainQuestReward;
        }
    }
}
