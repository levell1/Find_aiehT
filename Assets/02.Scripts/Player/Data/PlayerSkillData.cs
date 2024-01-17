using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public int SkillCost { get; private set; }
    [field: SerializeField][field: Range(5f, 30f)] public float SkillCoolTime { get; private set; }
    [field: SerializeField] public int SkillDamage { get; private set; }
    [field: SerializeField][field: Range(0f, 15f)] public float SkillRange { get; private set; }
}

[Serializable]
public class PlayerSkillData
{
    [field: SerializeField] public List<SkillInfoData> SkillInfoDatas { get; private set; }

}

[Serializable]
public class PlayerJsonSKillData
{
    [SerializeField] public List<SkillInfoData> PlayerSkillData;

    public void SetSkillData(List<SkillInfoData> newSkillData)
    {
        PlayerSkillData = newSkillData;
    }

}



