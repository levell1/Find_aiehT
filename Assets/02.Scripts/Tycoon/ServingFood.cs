using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;

    private GameObject _canHoldFood;
    private GameObject _holdingFood;
    private List<GameObject> _cleaningFoods = new();

    private bool _isHold = false;

    private const float _minDistanceToPutFood = 2f;

    public void TycoonInteraction()
    {
        if (_cleaningFoods.Count > 0)
        {
            OnCleaningFood();
        }
        else if (_canHoldFood != null || _holdingFood != null)
        {
            OnCatchFood();
        }
    }

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
        _canHoldFood = null;
    }

    private void PutdownFood()
    {
        float minDistance = _minDistanceToPutFood;
        FoodPlace foodPlace = null;

        // TODO
        foreach (GameObject station in TycoonManager.Instance.ServingStations)
        {
            FoodPlace stationFood = station.GetComponent<FoodPlace>();

            if (stationFood.CurrentFood == null)
            {
                float d = Vector3.Distance(_handTransform.position, station.transform.position);
                if (d < minDistance)
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
            _holdingFood = null;
        }
    }

    private void CleaningFood()
    {
        // TODO: Player Clean Anim, Player position 고정
        int lastIndex = _cleaningFoods.Count - 1;
        StartCoroutine(CleanFood(_cleaningFoods[lastIndex], lastIndex));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CookedFood"))
        {
            if (other.gameObject.GetComponent<CookedFood>().ShouldClean)
            {
                _cleaningFoods.Add(other.gameObject);
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
                if(_cleaningFoods.Find(obj => other.gameObject) == other.gameObject)
                {
                    _cleaningFoods.Remove(other.gameObject);
                }
            }
            else if (_canHoldFood == other.gameObject)
                _canHoldFood = null;
        }
    }

    #region Coroutine

    IEnumerator CleanFood(GameObject food, int index)
    {
        _cleaningFoods.RemoveAt(index);

        yield return new WaitForSeconds(0f);

        food.GetComponent<CookedFood>().CurrentFoodPlace = null;

        Destroy(food);
    }

    #endregion
}
