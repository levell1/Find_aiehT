
public class EnemyDailyQuest : Quest
{
    public EnemyDailyQuest(QuestSO data, int questNumber) : base(data ,questNumber)
    {
        for (int i = _minTargetID; i < _maxTargetID; i++)
        {
            _randomIDList.Add(i);
        }
        RandomQuest();
        EnemyTotalQuestReward = EnemyQuestReward * _player.Data.PlayerData.PlayerLevel;
    }

    protected override void InitQuest()
    {
        _minTargetID = EnemyDatas.EnemyList[0].EnemyID;

        _maxTargetID = EnemyDatas.EnemyList[^1].EnemyID;
        
        _maxTargetQuantity = _enemyQuestData.maxTargetQuantity;
    }

    public override string GetQuestTitle()
    {
        return string.Format($"사냥 퀘스트");
    }

    public override string GetQuestDescription()
    {
        foreach (var enemyData in EnemyDatas.EnemyList)
        {
            if (enemyData.EnemyID == TargetID)
            {
                _enemyName = enemyData.EnemyName;
                _enemyRegion = enemyData.EnemyRegion;
                break;
            }
        }

        return string.Format($"{_enemyName} 몬스터를 {TargetQuantity}마리 잡아라");

    }

    public override string GetQuestRegion()
    {
        return string.Format($"출몰 지역: {_enemyRegion}");
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
