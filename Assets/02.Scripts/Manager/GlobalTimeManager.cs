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
    public float StartTime; //게임시작시 한번만 사용되는 변수
    private float _totalHours;
    public float Day;
    public float Hour;
    public float Minutes;
    private bool _isChangeDay;
    private float _currentHour;
    private float _timeRate;

    private float _penaltyTime = 1f / 24f;
    public float NextMorning = 7f / 24f;
    public int EventCount;
    public TextMeshProUGUI TimeText;

    public bool IsItemRespawn = false;
    public bool IsActiveOutFieldUI;
    public bool IsMoveVillageToField;
    public event Action OnInitQuest;
    public event Action OnOutFieldUI;
    public event Action OnNightCheck;
    public event Action OnBossRespawn;

    private Coroutine _coroutine;

    private void Start()
    {
        _isChangeDay = true;
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    // TODO
    // DAY 기본값이 0일차, Load했을때는 Load한 날짜
    // 00시에 저장하니 로딩쪽에서 시간을 빼고 다시 더하는 작업때문에 날짜가 변해버림
    private void OnEnable()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;

        _timeRate = 1.0f / FullDayLength; //얼마큼씩 변하는지 계산 1/하루
        
        if(gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            float LoadTime = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveDayTime;
            //DayTime = LoadTime - (_penaltyTime) * 4;
            DayTime = LoadTime;
            Day = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveDay;
        }
        else if(gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            DayTime = StartTime;
        }


    }

    private void Update()
    {
        if (Hour < _currentHour)
        {
            _isChangeDay = false;
        }
         _currentHour = Hour;

        if (SceneManager.GetActiveScene().name != SceneName.TycoonScene && SceneManager.GetActiveScene().name != SceneName.TitleScene)
        {
            SetDayTime();
        }

        if(!_isChangeDay)
        {
            ChangeDay();
        }

        if (Hour == 18f && IsActiveOutFieldUI && SceneManager.GetActiveScene().name == SceneName.Field)
        {
            EventCount = 0;
            IsActiveOutFieldUI = false;
            OnOutFieldUI?.Invoke();
        }

        if(Hour >= 18f)
        {
            IsMoveVillageToField = false;
        }

        if(Hour == 23f && SceneManager.GetActiveScene().name == SceneName.Field)
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(WarningTimeMessage());
            }
        }

        //if (Hour == 23f && Minutes == 59f)
        //{
        //    EventCount = 1;
        //    OnOutFieldUI?.Invoke();
        //    DayTime = _nextMorning;
        //}
    }

    private void SetDayTime()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f;

        // 하루를 24시간으로 다시 나눠버리기~
        _totalHours = DayTime * 24f;
        Hour = Mathf.Floor(_totalHours);
        Minutes = Mathf.Floor((_totalHours - Hour) * 60f);

        string timeString = string.Format("{0}일차 {1:00}:{2:00}", Day, Hour, Minutes);
        if (TimeText != null)
        {
            TimeText.text = timeString;
        }
    }

    public void PenaltyTime()
    {
        DayTime += _penaltyTime;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        //DayTime += _penaltyTime;
        ItemRespawn();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void ChangeDay() 
    {
        _isChangeDay = true;
        IsActiveOutFieldUI = true;
        IsItemRespawn = false;
        IsMoveVillageToField = true;
        ++Day;
        OnInitQuest?.Invoke();
        OnBossRespawn?.Invoke();
    }

    public void NightChecker()
    {
        OnNightCheck?.Invoke();
    }

    public bool NightCheck()
    {
        if (Hour >= 18f)
        {
            OnNightCheck?.Invoke();
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

    public void TycoonToVillage()
    {
        DayTime = NextMorning;
    }

    public void ItemRespawn()
    {
        if (Hour >= 6f && !IsItemRespawn)
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

    private IEnumerator WarningTimeMessage()
    {
        PlayerInteraction playerInteraction = GameManager.Instance.Player.GetComponentInChildren<PlayerInteraction>();
        float oneHour = FullDayLength / 24f;

        playerInteraction.ErrorText.text = ErrorMessageTxt.OneHourLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 2f); //30분

        playerInteraction.ErrorText.text = ErrorMessageTxt.ThirtyMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f); //10분

        playerInteraction.ErrorText.text = ErrorMessageTxt.TwentyMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f); //10분

        playerInteraction.ErrorText.text = ErrorMessageTxt.TenMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f); //10분

        EventCount = 1;
        OnOutFieldUI?.Invoke();
        DayTime = NextMorning;

        _coroutine = null;
    }
}

