using System;
using UnityEngine;

[Serializable]
public class MainQuestData
{
    [field: SerializeField] public int QuestID;
    [field: SerializeField] public int Target;
    [field: SerializeField] public string Description;
    [field: SerializeField] public bool IsProgress;
    [field: SerializeField][field: Range(0, 3000)] public int Reward = 2000;
}
