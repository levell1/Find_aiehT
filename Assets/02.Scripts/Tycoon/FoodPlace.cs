using System;
using System.Collections;
using UnityEngine;

public class FoodPlace : MonoBehaviour
{
    #region Field
    public int SeatNum { get; set; }

    private Customer _currentCustomer = null;
    public Customer CurrentCustomer
    {
        get { return _currentCustomer; }
        set
        {
            if (value == null && _currentCustomer != null)
            {
                _currentCustomer.OnCustomerExit -= CustomerExit;
                _currentCustomer.OnSelectFood -= MatchCustomerFood;
            }
            if (value != null)
            {
                value.OnCustomerExit += CustomerExit;
                value.OnSelectFood += MatchCustomerFood;
            }

            _currentCustomer = value;
        }
    }

    private CookedFood _currentFood = null;
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
        if (_currentCustomer == null || _currentFood == null) return;
        if (_currentFood.ShouldClean) return;
        if (_currentCustomer.TargetFood.name != _currentFood._FoodSO.CookedFoodObject.name) return;

        OnCustomerGetFood?.Invoke();

        TycoonManager.Instance._TycoonUI.UpdateCurrentGold(_currentFood._FoodSO.Price);

        StartCoroutine(SetFoodToCleanState(_currentFood));
    }

    private void CustomerExit()
    {
        TycoonManager.Instance.CustomerExit(SeatNum);
    }

    #region Coroutine

    IEnumerator SetFoodToCleanState(CookedFood food)
    {
        yield return TycoonManager.Instance._waitForCustomerEatTime;

        food.ShouldClean = true;
    }

    #endregion
}
