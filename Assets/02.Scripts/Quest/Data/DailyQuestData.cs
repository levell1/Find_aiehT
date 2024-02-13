using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyQuestData
{
    [field: SerializeField] public int targetID;
    [field: SerializeField] public int maxTargetID;
    [field: SerializeField] [field: Range(0, 20)] public int maxTargetQuantity = 10;
    [field: SerializeField][field: Range(0, 700)] public int reward = 500;

}
