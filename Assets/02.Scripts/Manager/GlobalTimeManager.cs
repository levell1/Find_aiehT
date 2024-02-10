using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimeManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //인스펙터 창에서 0~1 스크롤로 조절 가능
    public float DayTime;
    public float FullDayLength;  //하루
    public float StartTime; //게임시작시 한번만 사용되는 변수
    private float _totalHours;
    public float Day;
    public float Hour;
    public float Minutes;
    private bool _isChangeDay;
    private float _timeRate;
    public TextMeshProUGUI TimeText;

    public bool IsItemRespawn = false;

    public event Action OnInitQuest;

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
        string timeString = string.Format("{0}일차 {1:00}:{2:00}", Day, Hour, Minutes);
        if (TimeText!=null)
        {
            TimeText.text = timeString;
        }
        
        ChangeDay();
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        DayTime += 0.5f / 24f;
        if (scene.name != SceneName.LoadingScene)
        {
            ItemRespawn();
        }
    }

    private void ChangeDay()
    {
        if (Hour == 0 && !_isChangeDay)
        {
            _isChangeDay = true;
            IsItemRespawn = false;
            ++Day;
            OnInitQuest?.Invoke();
        }
        else if (Hour == 1)
        {
            _isChangeDay = false;
        }
    }

    public bool NightCheck() //오전 0~6 , 오후 6~12
    {
        if (Hour <= 6f || 18f <= Hour)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EnterTycoonTime()
    {
        if (18f <= Hour && Hour <= 20f)
        {
            return true;
        }
        else if (Day == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ItemRespawn()
    {
        if (Hour == 6f && !IsItemRespawn)
        {
            List<int> keysToModify = new List<int>();

            foreach (var key in GameManager.Instance.DataManager.ItemWaitSpawnDict.Keys)
            {
                keysToModify.Add(key);
            }

            foreach (var key in keysToModify)
            {
                GameManager.Instance.DataManager.ItemWaitSpawnDict[key] = true;
            }

            IsItemRespawn = true;
        }
    }
}

