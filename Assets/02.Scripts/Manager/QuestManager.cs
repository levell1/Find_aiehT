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
        _enemyQuantityDict.Clear();
        _natureQuantityDict.Clear();

        int enemyCount = 10001;
        int natureCount = 20001;

        for (int i = 0; i < _questCount; i++) 
        { 
            if(i < _questCount / 2)
            {
                EnemyDailyQuest newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, enemyCount);
                ActiveQuests.Add(newEnemyQuest);
                _enemyQuantityDict.Add(enemyCount, newEnemyQuest.TargetQuantity);
                Debug.Log("퀘스트 " + enemyCount + " 내용: " + newEnemyQuest.GetQuestDescription());
                enemyCount++;
            }
            else // 나머지 절반은 Nature 퀘스트에 할당
            {
                NatureDailyQuest newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, natureCount);
                ActiveQuests.Add(newNatureQuest);
                _natureQuantityDict.Add(natureCount, newNatureQuest.TargetQuantity);
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

    // TODO 퀘스트를 수락하고 처리하는 메서드
    public void AcceptQuest(Quest quest)
    {
        if (!AcceptQuestList.Contains(quest))
        {
            AcceptQuestList.Add(quest);

            if (quest is EnemyDailyQuest)
            {
                EnemyHealthSystem.OnQuestTargetDie -= UpdateEnemyQuestProgress;
                EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
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
                    Debug.Log(questNumber);
                    if (_enemyQuantityDict.ContainsKey(questNumber))
                    {
                        _enemyQuantityDict[questNumber]--;
                        Debug.Log("수량 줄어줘:  " + _enemyQuantityDict[questNumber]);
                        if (_enemyQuantityDict[questNumber] <= 0)
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
            Debug.Log(quest.QuestNumber);

            if (quest is NatureDailyQuest)
            {
                NatureDailyQuest natureItemQuest = (NatureDailyQuest)quest;
                if (natureItemQuest.TargetID == targetID)
                {
                    // 퀘스트 번호 
                    int questNumber = natureItemQuest.QuestNumber;
                    Debug.Log(questNumber);
                   
                    if (_natureQuantityDict.ContainsKey(questNumber))
                    {
                        _natureQuantityDict[questNumber]--;
                        Debug.Log("수량 줄어줘:  " + _natureQuantityDict[questNumber]);
                        if (_natureQuantityDict[questNumber] <= 0)
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
        AcceptQuestList.Remove(quest);
        Debug.Log("퀘스트 완");

        if (quest is EnemyDailyQuest)
        {
            EnemyHealthSystem.OnQuestTargetDie -= UpdateEnemyQuestProgress;
        }
        else if (quest is NatureDailyQuest)
        {
            ItemObject.OnQuestTargetInteraction -= UpdateNatureQuestProgress;
        }


        foreach (Quest newquest in AcceptQuestList)
        {
            Debug.Log(newquest.QuestNumber);
        }

    }



}
