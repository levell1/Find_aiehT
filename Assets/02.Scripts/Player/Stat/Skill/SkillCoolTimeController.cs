using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeController : MonoBehaviour
{
    protected float _coolTime;
    [SerializeField] protected Image _coolTimeImage;
    protected CoolTimeManager _coolTimeManager; 

    public bool IsCoolTime { get; private set; }

    public virtual void Start()
    {
        IsCoolTime = false;
        _coolTimeManager = GameManager.Instance.CoolTimeManger;
    }

    public virtual void StartCoolTime(float coolTime)
    {
        if(!IsCoolTime)
        {
            _coolTime = coolTime;
            IsCoolTime = true;
        }
    }
    protected virtual void HandleCoolTimeFinish()
    {
        IsCoolTime = false;
    }

}
