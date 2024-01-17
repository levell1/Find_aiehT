using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeText : MonoBehaviour
{
    private TMP_Text _timeText;

    private int _timer = 510;

    private void Awake()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
        _timeText = GetComponent<TMP_Text>();
    }

    private void LoadedsceneEvent(Scene arg0, LoadSceneMode arg1)
    {
        _timer += 10;
        _timeText.text = (_timer / 60).ToString("D2") + ":" + (_timer % 60).ToString("D2");
        //≈∏¿ÃƒÔ, ªÁ∏¡, ªı∑ŒΩ√¿€ Ω√ timer = 540
    }

    private void Start()
    {
        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        _timer += 5;

        _timeText.text = (_timer / 60).ToString("D2") + ":" + (_timer % 60).ToString("D2") ;

        yield return new WaitForSeconds(10f);

        StartCoroutine(TimerCoroution());
    }
}
