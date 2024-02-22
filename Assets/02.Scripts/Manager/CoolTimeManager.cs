using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeManager : MonoBehaviour
{
    /// <summary>
    /// 쿨타임 이벤트를 저장하는 Dictionary 
    /// key: 쿨타임을 저장할 이름, value: 각각 이름으로 저장된 이벤트
    /// </summary>
    private Dictionary<string, Action> _coolTimeEvents = new Dictionary<string, Action>();


    public void StartCoolTimeCoroutine(string objName, float coolTime, Image? coolTimeImage)
    {
        StartCoroutine(CoolTimeController(objName, coolTime, coolTimeImage));
    }


    private IEnumerator CoolTimeController(string objName, float coolTime, Image? coolTimeImage)
    {
        float maxCool = coolTime;
        while (coolTime > 0f)
        {
            coolTime -= Time.deltaTime;

            if (coolTimeImage != null)
            {
                coolTimeImage.fillAmount = coolTime / maxCool;
            }
            yield return null;
        }


        if (_coolTimeEvents.ContainsKey(objName))
        {
            _coolTimeEvents[objName]?.Invoke();
        }
    }

    public void AddCoolTimeEvent(string objName, Action action)
    {
        if (!_coolTimeEvents.ContainsKey(objName))
        {
            _coolTimeEvents.Add(objName, action);
        }
        else
        {
            _coolTimeEvents[objName] += action;
        }
    }

    public void RemoveCoolTimeEvent(string objName, Action action)
    {
        if (_coolTimeEvents.ContainsKey(objName))
        {
            _coolTimeEvents[objName] -= action;
        }
    }
}
