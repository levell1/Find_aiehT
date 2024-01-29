using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    public TextMeshProUGUI AreaText;
    private string AreaInfo;

    public float WaitTime = 3f;
    public float DisableTime = 1f;

    private bool _isCo = false;

    private void Awake()
    {
        SceneManager.sceneLoaded += Enter;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Enter;
        StartCoroutine(FadeOutText());
    }

    private void OnDisable()
    {
        if (_isCo)
        {
            StopCoroutine(FadeOutText());
        }
        SceneManager.sceneLoaded -= Enter;
    }

    private void Enter(Scene scene, LoadSceneMode mode)
    {
        //TODO 다른방법 생각해보기
        if (scene.name == "KGM")
        {
            AreaInfo = "사냥터";
        }
        else if (scene.name == "KGM_TestVillage")
        {
            AreaInfo = "마을";
        }

        AreaText.text = string.Format("[ {0} ]", AreaInfo);
        gameObject.SetActive(true);
    }

    IEnumerator FadeOutText()
    {
        _isCo = true;
        yield return new WaitForSeconds(WaitTime);

        float _elapsedTime = 0f;
        while (_elapsedTime < DisableTime)
        {
            AreaText.alpha = Mathf.Lerp(1f, 0f, _elapsedTime / DisableTime);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        _isCo = false;
    }

}
