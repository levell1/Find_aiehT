using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimeManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //인스펙터 창에서 0~1 스크롤로 조절 가능
    public float DayTime;
    public float FullDayLength;  //하루
    public float StartTime = 0.2f; //게임시작시 한번만 사용되는 변수
    private float _totalHours;
    public float Hour;
    public float Minutes;
    private float _timeRate;
    public TextMeshProUGUI TimeText;

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        _timeRate = 1.0f / FullDayLength; //얼마큼씩 변하는지 계산 1/하루
        DayTime = StartTime;
    }

    private void Update()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f;
        // 하루를 24시간으로 다시 나눠버리기~
        _totalHours = DayTime * 24f;
        Hour = Mathf.Floor(_totalHours);
        Minutes = Mathf.Floor((_totalHours - Hour) * 60f);
        string timeString = string.Format("{0:00}:{1:00}", Hour, Minutes);
        TimeText.text = timeString;
    }

    void LoadedsceneEvent(Scene arg0, LoadSceneMode arg1) //씬이동 패널티
    {
        DayTime += 1f / 24f;
    }
}

