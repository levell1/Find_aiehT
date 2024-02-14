using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlotsUI : MonoBehaviour
{
    public Image SelectedFood;
    public Image CookingTimeUI;
    private float _time;

    private void OnEnable()
    {
        CookingTimeUI.fillAmount = 0f;
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
        CookingTimeUI.fillAmount = _time / TycoonManager.Instance._FoodCreater.FoodCreateDelayTime;

        if (_time >= TycoonManager.Instance._FoodCreater.FoodCreateDelayTime)
        {
            Destroy(gameObject);
        }
    }
}
