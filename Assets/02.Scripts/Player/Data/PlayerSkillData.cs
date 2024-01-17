using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SkillInfoData
{
    [field: SerializeField] private string SkillName;
    [field: SerializeField] private int SkillCost;
    [field: SerializeField][field: Range(5f, 30f)] private float SkillCoolTime;
    [field: SerializeField] private int SkillDamage;
    [field: SerializeField][field: Range(0f, 15f)] private float SkillRange;
}

[Serializable]
public class PlayerSkillData
{
    [field: SerializeField] public List<SkillInfoData> SkillInfoDatas;

}



