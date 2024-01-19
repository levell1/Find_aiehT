using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlace : MonoBehaviour
{
    #region Field

    private const float _foodDestroyTime = 2.0f;

    public int SeatNum { get; set; }
    
    private CustomerController _currentCustomer;
    public CustomerController CurrentCustomer
    {
        get { return _currentCustomer; }
        set { _currentCustomer = value; }
    }
    
    private CookedFood _currentFood;
    public CookedFood CurrentFood
    {
        get { return _currentFood; }
        set
        {
            _currentFood = value;

            if (_currentCustomer != null && _currentFood != null)
            {
                MatchWithCustomer();
            }
        }
    }

    #endregion

    #region event

    public event Action OnCustomerGetFood;

    #endregion
    
    private void MatchWithCustomer()
    {
        if (_currentCustomer.TargetFood.name != _currentFood.FoodName)
            return;

        // TODO: Get Gold
        
        OnCustomerGetFood.Invoke();

        // TODO: 음식을 다시 잡지 못하도록 수정
        StartCoroutine(DestoryCurrentFood(_currentFood.gameObject));
        
        _currentFood = null;
        _currentCustomer = null;
        GameManager.instance.TycoonManager.CustomerExit(SeatNum);
    }

    #region Coroutine

    IEnumerator DestoryCurrentFood(GameObject food)
    {
        yield return new WaitForSeconds(_foodDestroyTime);

        Destroy(food);
    }

    #endregion
}
