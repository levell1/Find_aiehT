using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDailyQuest : Quest
{
    public EnemyDailyQuest(DailyQuestData data, int questNumber) : base(data, questNumber)
    {
    }

    protected override void InitQuest()
    {
        _minTargetID = EnemyDatas.EnemyList[0].EnemyID;

        // 마지막 위치의 데이터를 불러오는 법
        _maxTargetID = EnemyDatas.EnemyList[^1].EnemyID;
        
        _maxTargetQuantity = QuestData.maxTargetQuantity;
    }

    public override string GetQuestTitle()
    {
        return string.Format($"사냥 퀘스트");
    }

    public override string GetQuestDescription()
    {
        return string.Format($"{_enemyName} 몬스터를 {TargetQuantity}마리 잡아라");
    }

    public override int GetTargetID()
    {
        return base.GetTargetID();
    }

    public override string GetQuestRewardToString()
    {
        return string.Format($"{base.GetQuestRewardToString()} EXP");
    }

}
