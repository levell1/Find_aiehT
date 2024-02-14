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

    // 쿨타임을 시작하는 메서드
    public void StartCoolTimeCoroutine(string objName, float coolTime, Image? coolTimeImage)
    {
        StartCoroutine(CoolTimeController(objName, coolTime, coolTimeImage));
    }

    // 쿨타임 코루틴
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

        //해당 이벤트 호출
        if (_coolTimeEvents.ContainsKey(objName))
        {
            _coolTimeEvents[objName]?.Invoke();
        }
    }

    // 쿨타임을 사용하는 곳에 이벤트를 추가하는 메서드
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

    // 이벤트 구독을 해제하는 함수
    public void RemoveCoolTimeEvent(string objName, Action action)
    {
        if (_coolTimeEvents.ContainsKey(objName))
        {
            _coolTimeEvents[objName] -= action;
        }
    }
}
