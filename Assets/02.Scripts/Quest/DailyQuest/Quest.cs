using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public DailyQuestData QuestData;
    protected int _minTargetID;
    protected int _maxTargetID;

    protected string _enemyName;
    protected string _natureItemName;

    protected int _minTargetQuantity = 1;
    protected int _maxTargetQuantity;

    protected int _questReward;

    public int QuestNumber;
    public int TargetID;
    public int TargetQuantity;

    public ItemDataListSO ItemDatas = GameManager.Instance.DataManager.ItemDataList;
    public EnemyDataListSO EnemyDatas = GameManager.Instance.DataManager.EnemyDataList;

    private List<int> _randomIDList =  new List<int>(); 

    public Quest(DailyQuestData data, int questNumber)
    {

        QuestData = data;
        QuestNumber = questNumber;
        _questReward = data.reward;
        InitQuest();

        for(int i = _minTargetID; i < _maxTargetID; i++)
        {
            _randomIDList.Add(i);
        }

        RandomQuest();
    }

    protected virtual void InitQuest() {}

    private void RandomQuest()
    {
        //TargetID = Random.Range(_minTargetID, _maxTargetID);
        TargetQuantity = Random.Range(_minTargetQuantity, _maxTargetQuantity);

        int index = Random.Range(0, _randomIDList.Count);

        TargetID = _randomIDList[index];
        _randomIDList.RemoveAt(index);

        foreach (var enemyData in EnemyDatas.EnemyList)
        {
            if (enemyData.EnemyID == TargetID)
            {
                // 해당 몬스터의 이름을 가져옴
                _enemyName = enemyData.EnemyName;
                Debug.Log("동물 이름: " + _enemyName);
                break;
            }
        }

        foreach (var natureItem in ItemDatas.ItemList)
        {
            if (natureItem.ItemID == TargetID)
            {
                // 해당 몬스터의 이름을 가져옴
                _natureItemName = natureItem.ObjName;
                Debug.Log("채집물 이름: " + _natureItemName);
                break;
            }
        }

    }

    public virtual string GetQuestTitle()
    {
        return string.Format("사냥 퀘스트");
    }

    //TODO 로그 추후에 UI로 변경
    public virtual string GetQuestDescription()
    {
        return string.Format($"몬스터 ID {_enemyName}을/를 가진 몬스터를 {TargetQuantity}마리 잡아라");
    }

    public virtual int GetTargetID()
    {
        return TargetID;
    }

    public virtual string GetQuestRewardToString()
    {
        return _questReward.ToString();
    }

}
