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
    public Dictionary<int, int> EnemyQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    public Dictionary<int, int> NatureQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할

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
        EnemyQuantityDict.Clear();
        NatureQuantityDict.Clear();

        int enemyQuestID = 10001;
        int natureQuestID = 20001;

        int currentQuantity = 0;

        for (int i = 0; i < _questCount; i++) 
        { 
            if(i < _questCount / 2)
            {
                //EnemyDailyQuest newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, enemyCount);

                // Do While로 처리한 이유: 퀘스트 내용이 중복되지 않게 하기 위함
                EnemyDailyQuest newEnemyQuest;
                do
                {
                    newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, enemyQuestID);
                } while (CheckForDuplicateTargetID(newEnemyQuest.TargetID));

                ActiveQuests.Add(newEnemyQuest);
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
                    newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, natureQuestID);
                } while (CheckForDuplicateTargetID(newNatureQuest.TargetID));

                ActiveQuests.Add(newNatureQuest);
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
