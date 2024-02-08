using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDailyQuest : Quest
{
    public EnemyDailyQuest(DailyQuestData data, int questNumber) : base(data)
    {
        QuestNumber = questNumber;
        Debug.Log(questNumber);
    }

    protected override void InitQuest()
    {
        _minTargetID = QuestData.targetID;
        _maxTargetID = QuestData.maxTargetID;
        _maxTargetQuantity = QuestData.maxTargetQuantity;
    }

    public override string GetQuestDescription()
    {
        return string.Format($"몬스터 ID {TargetID}를 가진 몬스터를 {TargetQuantity}마리 잡아라");
    }

}
