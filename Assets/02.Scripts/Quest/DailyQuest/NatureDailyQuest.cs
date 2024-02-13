using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureDailyQuest : Quest
{
    public NatureDailyQuest(DailyQuestData data, int questNumber) : base(data, questNumber)
    {
    }
    protected override void InitQuest()
    {
        _minTargetID = ItemDatas.ItemList[0].ItemID;

        // 마지막 위치의 데이터를 불러오는 법
        _maxTargetID = ItemDatas.ItemList[^1].ItemID;

        _maxTargetQuantity = QuestData.maxTargetQuantity;
    }

    public override string GetQuestTitle()
    {
        return string.Format("채집 퀘스트");
    }

    public override string GetQuestDescription()
    {
        return string.Format($"{_natureItemName} 채집물을 {TargetQuantity}개 채집해라");
    }

    public override int GetTargetID()
    {
        return base.GetTargetID();
    }
    public override string GetQuestRewardToString()
    {
        return string.Format($"{base.GetQuestRewardToString()} GOLD");
    }

}
