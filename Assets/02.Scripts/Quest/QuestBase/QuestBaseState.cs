using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBaseState : IQuestState
{
    protected Quest _quest;

    public QuestBaseState(Quest quest)
    {
        _quest = quest;
    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void UpdateState()
    {

    }

  
}
