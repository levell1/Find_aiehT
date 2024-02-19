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
    [field: SerializeField] private float SkillCoolTime;
    [field: SerializeField] private float SkillDamage;
    [field: SerializeField] private float SkillRange;
    [field: SerializeField] private float SkillDistance;
    [field: SerializeField] private string SkillPrefabsName;

    public string GetSkillName() { return SkillName; }
    public int GetSkillCost() { return SkillCost; }
    public float GetSkillDamage() { return SkillDamage; }
    public float GetSkillCoolTime() { return SkillCoolTime; }
    public float GetSkillRange() { return SkillRange; }
    public float GetSkillDistance() { return SkillDistance; }
    public string GetSkillPrefabsName() { return SkillPrefabsName; }

}

[Serializable]
public class PlayerSkillData
{
    public List<SkillInfoData> SkillInfoDatas;

    public SkillInfoData GetSkillData(int skillIndex) { return SkillInfoDatas[skillIndex]; }

}



