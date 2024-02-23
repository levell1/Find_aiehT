
public class NatureDailyQuest : Quest
{
    public NatureDailyQuest(QuestSO data, int questNumber) : base(data, questNumber)
    {
        for (int i = _minTargetID; i < _maxTargetID; i++)
        {
            _randomIDList.Add(i);
        }
        RandomQuest();
        if(GameManager.Instance.GameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            NatureToalQuestReward = NatureItemPrice * TargetQuantity * 5;
        }
    }

    protected override void InitQuest()
    {
        _minTargetID = ItemDatas.ItemList[0].ItemID;
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
        return string.Format($"{NatureToalQuestReward} GOLD");
    }

}
