using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureDailyQuest : Quest
{
    //private DailyQuestData _natureQuestData;
    public NatureDailyQuest(QuestSO data, int questNumber) : base(data, questNumber)
    {
        for (int i = _minTargetID; i < _maxTargetID; i++)
        {
            _randomIDList.Add(i);
        }
        RandomQuest();
    }

    protected override void InitQuest()
    {
        _minTargetID = ItemDatas.ItemList[0].ItemID;

        // 마지막 위치의 데이터를 불러오는 법
        _maxTargetID = ItemDatas.ItemList[^1].ItemID;

        _maxTargetQuantity = _natureQuestData.maxTargetQuantity;
    }

    public override string GetQuestTitle()
    {
        return string.Format("채집 퀘스트");
    }

    public override string GetQuestDescription()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            foreach (var natureItem in ItemDatas.ItemList)
            {
                if (natureItem.ItemID == TargetID)
                {
                    _natureItemName = natureItem.ObjName;
                    break;
                }
            }
        }

        return string.Format($"{_natureItemName} 채집물을 {TargetQuantity}개 채집해라");
    }

    public override int GetTargetID()
    {
        return base.GetTargetID();
    }
    public override string GetQuestRewardToString()
    {
        int questReward = (NatureQuestReward * TargetQuantity) / 2;
        return string.Format($"{questReward} GOLD");
    }

}
