using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUI : MonoBehaviour
{
    private Canvas _cookingUIcanvas;
    public GameObject FoodUIPrefab;
    public Transform Content;
    public List<CookingSlotsUI> CookingSlotsUIs = new List<CookingSlotsUI>();
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
        CookingSlotsUI cookingSlotsUI = FoodUIPrefab.GetComponent<CookingSlotsUI>();
        cookingSlotsUI.SelectedFood.sprite = foodSO.FoodSprite;
        //TODO 풀링으로 바꾸기
        CookingSlotsUIs.Add(Instantiate(cookingSlotsUI, Content));
    }
}
