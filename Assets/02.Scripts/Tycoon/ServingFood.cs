using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;

    private GameObject _canHoldFood;
    private GameObject _holdingFood;
    private bool _isHold = false;
    private const float _minDistanceToPutFood = 1.3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnCatchFood();
        }
    }

    // TODO: Change to InputSystem
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

    private void PickupFood()
    {
        if (_canHoldFood == null)
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
        foreach (GameObject station in GameManager.instance.TycoonManager.ServingStations)
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
            _holdingFood.transform.SetParent(foodPlace.transform);
            foodPlace.CurrentFood = _holdingFood.GetComponent<CookedFood>();
            _isHold = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CookedFood"))
        {
            _canHoldFood = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CookedFood"))
        {
            if (_canHoldFood == other.gameObject)
                _canHoldFood = null;
        }
    }
}
