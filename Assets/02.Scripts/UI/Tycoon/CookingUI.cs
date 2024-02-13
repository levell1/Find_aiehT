using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour
{
    private Canvas _cookingUIcanvas;
    public GameObject FoodUIPrefab;
    public Transform Content;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _cookingUIcanvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        _cookingUIcanvas.transform.LookAt(_cookingUIcanvas.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }

    public void StartCooking(FoodSO foodSO)
    {
        CookingSlotsUI a = FoodUIPrefab.GetComponent<CookingSlotsUI>();
        a.SelectedFood.sprite = foodSO.FoodSprite;
        //TODO 풀링으로 바꾸기
        Instantiate(a, Content);
    }
}
