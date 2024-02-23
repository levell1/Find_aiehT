public class MainQuest : Quest
{
    private MainQuestData _mainQuestData;
    private string _mainQuestDescription;
    public MainQuest(QuestSO data, int questNumber, int index) : base(data, questNumber)
    {
        _mainQuestData = data.MainQuestData[index];
        TargetQuantity = data.MainQuestData[index].Target;
        IsProgress = data.MainQuestData[index].IsProgress;
        MainQuestReward = data.MainQuestData[index].Reward;

        InitMainQuestData();

    }

    private void InitMainQuestData()
    {
        _mainQuestDescription = _mainQuestData.Description;

        GetQuestDescription();
    }

    public override string GetQuestTitle()
    {
        return string.Format("업 적");
    }


    public override string GetQuestDescription()
    {
        return string.Format($"{_mainQuestDescription}");
    }

    public override string GetQuestRewardToString()
    {
        return string.Format($"{MainQuestReward} GOLD");
    }
}
