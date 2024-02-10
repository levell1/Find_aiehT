using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureDailyQuest : Quest
{
    public NatureDailyQuest(DailyQuestData data, int questNumber) : base(data)
    {
    }
    protected override void InitQuest()
    {
        //_maxTargetID = QuestData.targetID;
        _minTargetID = QuestData.targetID;
        _maxTargetID = QuestData.maxTargetID;
        _maxTargetQuantity = QuestData.maxTargetQuantity;
    }

    public override string GetQuestDescription()
    {
        return string.Format($"채집물 ID {TargetID}를 가진 채집물을 {TargetQuantity}개 채집해라");
    }

}
