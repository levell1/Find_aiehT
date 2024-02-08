using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletedState : QuestBaseState
{
    public QuestCompletedState(Quest quest) : base(quest)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("퀘스트 완료 보상 출력");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("퀘스트 비우기");

        //TODO 메인이면 그다음 퀘스트로 진행
    }
}
