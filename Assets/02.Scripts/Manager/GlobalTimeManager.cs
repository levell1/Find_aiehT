using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimeManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)] 
    public float DayTime;
    public float FullDayLength;  
    public float StartTime; 
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
    public bool IsMoveFieldToVillage;
    public event Action OnInitQuest;
    public event Action OnOutFieldUI;
    public event Action OnNightCheck;
    public event Action OnBossRespawn;
    public event Action OnInitMainQuest;

    private Coroutine _coroutine;

    private void Start()
    {
        _isChangeDay = true;
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void OnEnable()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;

        _timeRate = 1.0f / FullDayLength; 
        
        if(gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            float LoadTime = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveDayTime;

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
        if (DayTime < _currentHour)
        {
            _isChangeDay = false;
        }
         _currentHour = DayTime;

        if (SceneManager.GetActiveScene().name != SceneName.TycoonScene && SceneManager.GetActiveScene().name != SceneName.TutorialScene
            && SceneManager.GetActiveScene().name != SceneName.DungeonScene)
        {
            SetDayTime();
        }

        if(!_isChangeDay)
        {
            ChangeDay();
        }

        if (Hour == 18f && IsActiveOutFieldUI && SceneManager.GetActiveScene().name == SceneName.HuntingScene)
        {
            EventCount = 0;
            IsActiveOutFieldUI = false;
            OnOutFieldUI?.Invoke();
        }

        if(Hour >= 18f)
        {
            IsMoveVillageToField = false;
            IsMoveFieldToVillage = true;
        }

        if(Hour == 23f && SceneManager.GetActiveScene().name == SceneName.HuntingScene)
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(WarningTimeMessage());
            }
        }


        if (Hour == 23f && Minutes == 59f)
        {
            DayTime += 0.01f / 24f;
            GoodMorning();
        }
    }

    private void SetDayTime()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f;

        _totalHours = DayTime * 24f;
        Hour = Mathf.Floor(_totalHours);
        Minutes = Mathf.Floor((_totalHours - Hour) * 60f);

        string timeString = string.Format("{0}일차 {1:00}:{2:00}", Day, Hour, Minutes);
        if (TimeText != null)
        {
            TimeText.text = timeString;
        }
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
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
        IsMoveFieldToVillage = false;
        ++Day;
        OnInitQuest?.Invoke();
        OnBossRespawn?.Invoke();

        if (Day == 1)
        {
            OnInitMainQuest?.Invoke();
        }

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
        
        if (07f <= Hour && Hour <= 24f)
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
        GoodMorning();
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
        yield return new WaitForSeconds(oneHour / 2f); 

        playerInteraction.ErrorText.text = ErrorMessageTxt.ThirtyMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f); 

        playerInteraction.ErrorText.text = ErrorMessageTxt.TwentyMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f);

        playerInteraction.ErrorText.text = ErrorMessageTxt.TenMinutesLeft;
        StartCoroutine(playerInteraction.ErrorMessage());
        yield return new WaitForSeconds(oneHour / 6f); 

        GoodMorning();

        _coroutine = null;
    }

    public void GoodMorning()
    {
        EventCount = 1;
        OnOutFieldUI?.Invoke();
    }
}

