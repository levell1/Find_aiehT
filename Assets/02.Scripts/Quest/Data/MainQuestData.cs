using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainQuestData
{
    [field: SerializeField] public int QuestID;
    [field: SerializeField] public int Target;
    [field: SerializeField] public string Description;
    [field: SerializeField][field: Range(0, 3000)] public int Reward = 2000;
}
