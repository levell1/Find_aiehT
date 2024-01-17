using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlace : MonoBehaviour
{
    private const float _foodDestroyTime = 2.0f;
    
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
                if (_currentCustomer.TargetFood.name == _currentFood.FoodName)
                {
                    MatchWithCustomer();
                }
            }
        }
    }

    public event Action OnCustomerGetFood;

    private void MatchWithCustomer()
    {
        // TODO: Get Gold

        OnCustomerGetFood.Invoke();

        StartCoroutine(DestoryCurrentFood(_currentFood.gameObject));
        
        _currentFood = null;
        _currentCustomer = null;
    }

    IEnumerator DestoryCurrentFood(GameObject food)
    {
        yield return new WaitForSeconds(_foodDestroyTime);

        Destroy(food);
    }
}
