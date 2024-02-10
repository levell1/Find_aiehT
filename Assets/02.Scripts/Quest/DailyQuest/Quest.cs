using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public DailyQuestData QuestData;
    protected int _minTargetID;
    protected int _maxTargetID;

    protected int _minTargetQuantity = 1;
    protected int _maxTargetQuantity;

    public int QuestNumber;
    public int TargetID;
    public int TargetQuantity;

    public Quest(DailyQuestData data)
    {
        QuestData = data;
        InitQuest();
        RandomQuest();
    }

    protected virtual void InitQuest() {}

    private void RandomQuest()
    {
        TargetID = Random.Range(_minTargetID, _maxTargetID);
        TargetQuantity = Random.Range(_minTargetQuantity, _maxTargetQuantity);
    }

    //TODO 로그 추후에 UI로 변경
    public virtual string GetQuestDescription()
    {
        return string.Format($"몬스터 ID {TargetID}를 가진 몬스터를 {TargetQuantity}마리 잡아라");
    }

}
