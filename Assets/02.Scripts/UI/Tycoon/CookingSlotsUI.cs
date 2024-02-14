using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlotsUI : MonoBehaviour
{
    private CookingUI _cookingUI;
    public Image SelectedFood;
    public Image CookingTimeUI;
    private float _time;

    private void Awake()
    {
        _cookingUI = TycoonManager.Instance.CookingUI;
    }

    private void OnEnable()
    {
        CookingTimeUI.fillAmount = 0f;
    }

    private void FixedUpdate()
    {
        if (_cookingUI.CookingSlotsUIs[0] == this)
        {
            _time += Time.deltaTime;
            CookingTimeUI.fillAmount = _time / TycoonManager.Instance._FoodCreater.FoodCreateDelayTime;

            if (_time >= TycoonManager.Instance._FoodCreater.FoodCreateDelayTime)
            {
                _cookingUI.CookingSlotsUIs.Remove(_cookingUI.CookingSlotsUIs[0]);
                Destroy(gameObject);
            }
        }
    }
}
