using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInProgressState : QuestBaseState
{
    public QuestInProgressState(Quest quest) : base(quest)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("퀘스트 시작");

    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("퀘스트 진행중");
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("퀘스트 완료");
    }

}
