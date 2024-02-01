using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeController : MonoBehaviour
{
    protected float _coolTime;
    [SerializeField] protected Image _coolTimeImage;

    public bool IsCoolTime { get; private set; }

    public virtual void Start()
    {
        IsCoolTime = false;
    }

    public virtual void StartCoolTime(float coolTime)
    {
        if(!IsCoolTime)
        {
            _coolTime = coolTime;
            StartCoroutine(CoolTimeCoroutine());
        }
    }


    private IEnumerator CoolTimeCoroutine()
    {
        IsCoolTime = true;
        float maxcool = _coolTime;
        while (_coolTime > 0f)
        {
           _coolTime -= Time.deltaTime;
           _coolTimeImage.fillAmount = _coolTime / maxcool;
           yield return null;
        }

        IsCoolTime = false;
    }

}
