using System;
using System.Collections;
using UnityEngine;

public class FoodPlace : MonoBehaviour
{
    #region Field

    // Eat Time 과 맞추기
    private const float _foodEatTime = 10.0f;

    public int SeatNum { get; set; }

    private CustomerController _currentCustomer = null;
    public CustomerController CurrentCustomer
    {
        get { return _currentCustomer; }
        set
        {
            if (value == null)
            {
                if (_currentCustomer != null)
                {
                    _currentCustomer.OnCustomerExit -= CustomerExit;
                }
            }

            _currentCustomer = value;

            if (_currentCustomer != null)
            {
                _currentCustomer.OnCustomerExit += CustomerExit;
            }
        }
    }

    private CookedFood _currentFood;
    public CookedFood CurrentFood
    {
        get { return _currentFood; }
        set
        {
            _currentFood = value;
            if (_currentFood != null)
            {
                _currentFood.CurrentFoodPlace = this;
            }

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
        if (_currentCustomer.TargetFoodName != _currentFood.FoodName)
            return;

        // TODO: Get Gold
        TycoonManager.instance._TycoonUI.UpdateCurrentGold(_currentFood._FoodSO.Price);

        OnCustomerGetFood.Invoke();

        // TODO: Object Pool
        _currentFood.CanHold = false;
        StartCoroutine(SetFoodToCleanState(_currentFood));
    }

    private void CustomerExit()
    {
        TycoonManager.instance.CustomerExit(SeatNum);
    }

    #region Coroutine

    IEnumerator SetFoodToCleanState(CookedFood food)
    {
        yield return new WaitForSeconds(_foodEatTime);

        food.ShouldClean = true;
    }

    #endregion
}
