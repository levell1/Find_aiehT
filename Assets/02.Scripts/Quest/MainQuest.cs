using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuest : Quest
{
    private MainQuestData _mainQuestData;
    private string _mainQuestDescription;

    public MainQuest(QuestSO data, int questNumber, int index) : base(data, questNumber)
    {
        _mainQuestData = data.MainQuestData[index];

        InitMainQuestData();

    }

    private void InitMainQuestData()
    {
        _mainQuestDescription = _mainQuestData.Description;

        GetQuestDescription();
    }

    public override string GetQuestTitle()
    {
        return string.Format("메인 퀘스트");
    }


    public override string GetQuestDescription()
    {
        // 손님 화 안내고 클리어
        // 누적 골드 5만
        // 던전 1회 입장
        // 불닭 1회 잡기
        return string.Format($"{_mainQuestDescription}");
    }

    public override string GetQuestRewardToString()
    {
        return string.Format($"{_mainQuestData.Reward} GOLD");
    }
}
