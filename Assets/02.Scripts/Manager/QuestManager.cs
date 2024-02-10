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
    [SerializeField] private int _questCount = 2;

    private List<Quest> activeQuests = new List<Quest>(); // 진행 중인 퀘스트 목록

    /// <summary>
    /// Key: 퀘스트 번호, Value: 수량
    /// </summary>
    private Dictionary<int, int> _enemyQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    private Dictionary<int, int> _natureQuantityDict = new Dictionary<int, int>(); //진행중인 퀘스트의 수량을 담고, 삭제하는 역할
    private void Start()
    {
        GameManager.Instance.GlobalTimeManager.OnInitQuest += InitializeQuest;
        EnemyHealthSystem.OnQuestTargetDie += UpdateEnemyQuestProgress;
        ItemObject.OnQuestTargetInteraction += UpdateNatureQuestProgress;
    }

    // 퀘스트를 초기화하고 추가하는 메서드
    public void InitializeQuest()
    {
        activeQuests.Clear();
        _enemyQuantityDict.Clear();
        _natureQuantityDict.Clear();

        for (int i = 0; i < _questCount; i++) 
        { 
            Quest newEnemyQuest = new EnemyDailyQuest(QuestSO.EnemyQuestData, i);
            Quest newNatureQuest = new NatureDailyQuest(QuestSO.NatureQuestData, i);
            activeQuests.Add(newEnemyQuest);
            activeQuests.Add(newNatureQuest);

            _enemyQuantityDict.Add(i, newEnemyQuest.TargetQuantity);
            _natureQuantityDict.Add(i, newNatureQuest.TargetQuantity);

            Debug.Log(_enemyQuantityDict[i]);

            Debug.Log("퀘스트 " + (i + 1) + " 내용: " + newEnemyQuest.GetQuestDescription());
            Debug.Log("퀘스트 " + (i + 1) + " 내용: " + newNatureQuest.GetQuestDescription());
        }
        //Quest newQuest = new Quest(QuestSO);
        //activeQuests.Add(newQuest);
        //activeQuests.Add(newQuest2);
        Debug.Log("퀘스트 초기화");
    }

    // TODO 퀘스트 진행 관리
    public void UpdateEnemyQuestProgress(int targetID)
    {

        foreach(Quest quest in activeQuests)
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
        foreach (Quest quest in activeQuests)
        {
            if (quest is NatureDailyQuest)
            {
                NatureDailyQuest natureQuest = (NatureDailyQuest)quest;
                Debug.Log("아이디: " + natureQuest.TargetID);
                Debug.Log("채집물: " + targetID);
            }
        }
    }

    //TODO 퀘스트를 완료하고 보상을 처리
    public void CompleteQuest(Quest quest)
    {
        // 퀘스트 완료시 보상 등등
        activeQuests.Remove(quest);
        Debug.Log("퀘스트 완");

        foreach (Quest newquest in activeQuests)
        {
            Debug.Log(newquest.QuestNumber);
        }

    }



}
