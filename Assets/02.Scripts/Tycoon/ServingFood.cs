using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;

    private GameObject _canHoldFood;
    private GameObject _holdingFood;
    private GameObject _cleaningFood;

    private bool _isHold = false;
    private bool _isPossibleToClean = false;

    private const float _minDistanceToPutFood = 1.3f;

    public void TycoonInteraction()
    {
        if (_isPossibleToClean)
        {
            OnCleaningFood();
        }
        else if (_canHoldFood != null || _holdingFood != null)
        {
            OnCatchFood();
        }
    }


    // TODO: Change to InputSystem - OnCatchFood, OnCleaningFood
    public void OnCatchFood()
    {
        if (!_isHold)
        {
            PickupFood();
        }
        else
        {
            PutdownFood();
        }
    }

    public void OnCleaningFood()
    {
        if (!_isHold)
        {
            CleaningFood();
            _isPossibleToClean = false;
        }
    }

    private void PickupFood()
    {
        if (_canHoldFood == null || !_canHoldFood.GetComponent<CookedFood>().CanHold)
            return;

        _canHoldFood.GetComponentInParent<FoodPlace>().CurrentFood = null;

        _holdingFood = _canHoldFood;
        _holdingFood.transform.position = _handTransform.position;
        _holdingFood.transform.SetParent(_handTransform);
        _isHold = true;
    }

    private void PutdownFood()
    {
        float minDistance = Mathf.Infinity;
        FoodPlace foodPlace = null;

        // TODO
        foreach (GameObject station in TycoonManager.Instance.ServingStations)
        {
            FoodPlace stationFood = station.GetComponent<FoodPlace>();

            if (stationFood.CurrentFood == null)
            {
                float d = Vector3.Distance(_handTransform.position, station.transform.position);
                if (d < minDistance && d < _minDistanceToPutFood)
                {
                    minDistance = d;
                    foodPlace = stationFood;
                }
            }
        }

        if (foodPlace != null)
        {
            _holdingFood.transform.position = foodPlace.gameObject.transform.position;
            // TODO: Rotation 고정?
            _holdingFood.transform.SetParent(foodPlace.transform);
            foodPlace.CurrentFood = _holdingFood.GetComponent<CookedFood>();
            _isHold = false;
        }
    }

    private void CleaningFood()
    {
        // TODO: Player Clean Anim, Player position 고정
        StartCoroutine(CleanFood(_cleaningFood));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CookedFood"))
        {
            if (other.gameObject.GetComponent<CookedFood>().ShouldClean)
            {
                _isPossibleToClean = true;
                _cleaningFood = other.gameObject;
            }
            else
            {
                _canHoldFood = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("CookedFood"))
        {
            if (other.gameObject.GetComponent<CookedFood>().ShouldClean)
            {
                _isPossibleToClean = false;
                _cleaningFood = null;
            }
            else if (_canHoldFood == other.gameObject)
                _canHoldFood = null;
        }
    }

    #region Coroutine

    IEnumerator CleanFood(GameObject food)
    {
        yield return new WaitForSeconds(0f);

        food.GetComponent<CookedFood>().CurrentFoodPlace = null;

        Destroy(food);
    }

    #endregion
}
