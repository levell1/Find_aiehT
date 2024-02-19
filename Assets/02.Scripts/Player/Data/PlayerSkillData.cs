using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SkillInfoData
{
    [field: SerializeField] private string _skillName;
    [field: SerializeField] private int _skillCost;
    [field: SerializeField] private float _skillCoolTime;
    [field: SerializeField] private float _skillDamage;
    [field: SerializeField] private float _skillRange;
    [field: SerializeField] private float _skillDistance;
    [field: SerializeField] private string _skillPrefabsName;

    public string SkillName
    {
        get { return _skillName; }
        set { _skillName = value; }
    }

    public int SKillCost
    {
        get { return _skillCost; }
        set { _skillCost = value; }
    }

    public float SkillCoolTime
    {
        get { return _skillCoolTime ; }
        set { _skillCoolTime = value; }
    }
    public float SkillDamage
    {
        get { return _skillDamage; }
        set { _skillDamage = value; }
    }
    public float SkillRange
    {
        get { return _skillRange; }
        set { _skillRange = value; }
    }
    public float SkillDistance
    {
        get { return _skillDistance; }
        set { _skillDistance = value; }
    }

    public string SkillPrefabsName
    {
        get { return _skillPrefabsName; }
        set { _skillPrefabsName = value; }
    }

}

    //public string GetSkillName() { return SkillName; }
    //public int GetSkillCost() { return SkillCost; }
    //public float GetSkillDamage() { return SkillDamage; }
    //public float GetSkillCoolTime() { return SkillCoolTime; }
    //public float GetSkillRange() { return SkillRange; }
    //public float GetSkillDistance() { return SkillDistance; }
    //public string GetSkillPrefabsName() { return SkillPrefabsName; }



[Serializable]
public class PlayerSkillData
{
    public List<SkillInfoData> SkillInfoDatas;

    public SkillInfoData GetSkillData(int skillIndex) { return SkillInfoDatas[skillIndex]; }

}



