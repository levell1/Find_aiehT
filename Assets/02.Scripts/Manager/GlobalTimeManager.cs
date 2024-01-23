using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //인스펙터 창에서 0~1 스크롤로 조절 가능
    public float DayTime;
    public float FullDayLength;  //하루
    public float StartTime = 0.4f; //게임시작시 한번만 사용되는 변수
    private float _timeRate;

    private void Start()
    {
        _timeRate = 1.0f / FullDayLength; //얼마큼씩 변하는지 계산 1/하루
        DayTime = StartTime;
    }

    private void Update()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f; //퍼센티지로 사용하기 위해 1.0f로 나눈다. 0 ~ 0.9999 까지만 사용가능
    }
}

