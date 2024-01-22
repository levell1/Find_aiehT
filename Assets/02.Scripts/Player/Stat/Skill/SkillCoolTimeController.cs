using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTimeController : MonoBehaviour
{
    protected float _coolTime;

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

        while (_coolTime > 0f)
        {
           _coolTime -= Time.deltaTime;
        
          yield return null;
        }

        IsCoolTime = false;
    }

}
