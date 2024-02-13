using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestTarget
{
    ENEMY,
    NATURE
}

public class QuestManager : MonoBehaviour
{
    public QuestSO QuestSO; // 퀘스트 데이터를 저장하고 있는 ScriptableObject

    [SerializeField] private int _questCount = 4;

    public List<Quest> ActiveQuests = new List<Quest>(); // 퀘스트 목록
    public List<Quest> AcceptQuestList = new List<Quest>(); // 수락한 퀘스트 목록

    public delegate void QuestAcceptedEvent(List<Quest> acceptedQuests);
    public event QuestAcceptedEvent OnQuestAccepted;

    public event Action<int, int> OnQuestValueUpdate;

    /// <summary>
    /// Key: 퀘스트 번호, Value: 수량
    /// </summary>
    private Dictionary<int, int> _enemyQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    private Dictionary<int, int> _natureQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할

    private void Start()
    {
        GameManager.Instance.GlobalTimeManager.OnInitQuest += InitializeQuest;
        //EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
        //ItemObject.OnQuestTargetInteraction += UpdateNatureQuestProgress;
    }

    // 퀘스트를 초기화하고 추가하는 메서드
    public void InitializeQuest()
    {
        ActiveQuests.Clear();
        AcceptQuestList.Clear();
        _enemyQuantityDict.Clear();
        _natureQuantityDict.Clear();

        int enemyCount = 10001;
        int natureCount = 20001;

        int currentQuantity = 0;

        for (int i = 0; i < _questCount; i++) 
        { 
            if(i < _questCount / 2)
            {
                //EnemyDailyQuest newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, enemyCount);

                EnemyDailyQuest newEnemyQuest;
                do
                {
                    newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, enemyCount);
                } while (CheckForDuplicateTargetID(newEnemyQuest.TargetID));

                ActiveQuests.Add(newEnemyQuest);
                _enemyQuantityDict.Add(enemyCount, currentQuantity);
                Debug.Log("퀘스트 " + enemyCount + " 내용: " + newEnemyQuest.GetQuestDescription());
                enemyCount++;
            }
            else // 나머지 절반은 Nature 퀘스트에 할당
            {
                //NatureDailyQuest newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, natureCount);
                NatureDailyQuest newNatureQuest;
                do
                {
                    newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, natureCount);
                } while (CheckForDuplicateTargetID(newNatureQuest.TargetID));

                ActiveQuests.Add(newNatureQuest);
                _natureQuantityDict.Add(natureCount, currentQuantity);
                Debug.Log("퀘스트 " + natureCount + " 내용: " + newNatureQuest.GetQuestDescription());
                natureCount++;
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

    // 중복확인
    private bool CheckForDuplicateTargetID(int targetID)
    {
        foreach (var quest in ActiveQuests)
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
            OnQuestAccepted?.Invoke(AcceptQuestList);

            if (quest is EnemyDailyQuest)
            {
                EnemyHealthSystem.OnQuestTargetDie -= UpdateEnemyQuestProgress;
                EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
                Debug.Log("몬스터 퀘스트 수락 및 이벤트 등록: ");
            }
            else if (quest is NatureDailyQuest)
            {
                ItemObject.OnQuestTargetInteraction -= UpdateNatureQuestProgress;
                ItemObject.OnQuestTargetInteraction += UpdateNatureQuestProgress;
                Debug.Log("채집물 퀘스트 수락 및 이벤트 등록: ");
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
                    if (_enemyQuantityDict.ContainsKey(questNumber))
                    {
                        _enemyQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(_enemyQuantityDict[questNumber], targetID);

                        Debug.Log("수량 줄어줘:  " + _enemyQuantityDict[questNumber]);
                        if (_enemyQuantityDict[questNumber] >= enemyQuest.TargetQuantity)
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
                    if (_natureQuantityDict.ContainsKey(questNumber))
                    {
                        _natureQuantityDict[questNumber]++;
                        OnQuestValueUpdate?.Invoke(_natureQuantityDict[questNumber], targetID);

                        Debug.Log("수량 줄어줘:  " + _natureQuantityDict[questNumber]);
                        if (_natureQuantityDict[questNumber] >= natureItemQuest.TargetQuantity)
                        {
                            // 퀘스트 삭제
                            CompleteQuest(quest);
                        }
                    }
                    break;
                }
            }

            //if (quest is NatureDailyQuest)
            //{
            //    NatureDailyQuest natureQuest = (NatureDailyQuest)quest;
            //    Debug.Log("아이디: " + natureQuest.TargetID);
            //    Debug.Log("채집물: " + targetID);
            //}
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

    }

    // 골드 보상 또는 경험치보상
    private void QuestReward(Quest quest)
    {
        Player player = GameManager.Instance.Player.GetComponent<Player>();

        if (quest is EnemyDailyQuest)
        {
            player.PlayerExpSystem.GetExpPlus(quest.QuestData.reward);
        }
        else if(quest is NatureDailyQuest)
        {
            int rewardGold = player.Data.PlayerData.GetPlayerGold() + quest.QuestData.reward;
            player.Data.PlayerData.SetPlayerGold(rewardGold);
        }
    }



}
