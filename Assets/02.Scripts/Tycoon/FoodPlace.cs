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
                MatchCustomerFood();
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
                MatchCustomerFood();
            }
        }
    }

    #endregion

    #region event

    public event Action OnCustomerGetFood;

    #endregion

    private void MatchCustomerFood()
    {
        if (_currentCustomer == null
            || _currentFood == null
            ||_currentCustomer.TargetFoodName != _currentFood._FoodSO.CookedFoodObject.name)
            return;

        // TODO: Get Gold
        TycoonManager.Instance._TycoonUI.UpdateCurrentGold(_currentFood._FoodSO.Price);

        OnCustomerGetFood.Invoke();

        // TODO: Object Pool
        _currentFood.CanHold = false;
        StartCoroutine(SetFoodToCleanState(_currentFood));
    }

    private void CustomerExit()
    {
        TycoonManager.Instance.CustomerExit(SeatNum);
    }

    #region Coroutine

    IEnumerator SetFoodToCleanState(CookedFood food)
    {
        yield return new WaitForSeconds(_foodEatTime);

        food.ShouldClean = true;
    }

    #endregion
}
