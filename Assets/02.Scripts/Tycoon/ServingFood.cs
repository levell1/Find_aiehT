using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;

    private GameObject _canHoldFood;
    private GameObject _holdingFood;
    private List<GameObject> _canCleaningFoods = new();

    private const float _minDistanceToPutFood = 2f;

    private WaitForSeconds _waitCleaningTime = new WaitForSeconds(0f);
    public bool CanThrowAway { get; set; }
    public bool CanOpenRecipeUI { get; set; }
    public void TycoonInteraction()
    {
        if (CanOpenRecipeUI)
        {
            GameManager.Instance.UIManager.ShowCanvas(UIName.RestaurantUI);
        }

        if (_holdingFood != null)
        {
            if (CanThrowAway)
                ThrowAwayFood();
            else
                PutdownFood();
        }
        else if (_canCleaningFoods.Count > 0)
        {
            CleaningFood();
        }
        else if (_canHoldFood != null)
        {
            PickupFood();
        }
    }

    private void PickupFood()
    {
        _canHoldFood.GetComponentInParent<FoodPlace>().CurrentFood = null;

        _holdingFood = _canHoldFood;
        _canHoldFood = null;

        _holdingFood.transform.position = _handTransform.position;
        _holdingFood.transform.SetParent(_handTransform);
        _holdingFood.GetComponent<Collider>().enabled = false;
    }

    private void PutdownFood()
    {
        float minDistance = _minDistanceToPutFood;
        FoodPlace foodPlace = null;

        // TODO
        //foreach (GameObject station in TycoonManager.Instance.ServingStations)
        //{
        //    FoodPlace stationFood = station.GetComponent<FoodPlace>();

        //    if (stationFood.CurrentFood == null)
        //    {
        //        float d = Vector3.Distance(_handTransform.position, station.transform.position);
        //        if (d < minDistance)
        //        {
        //            minDistance = d;
        //            foodPlace = stationFood;
        //        }
        //    }
        //}

        for (int i = 0; i < TycoonManager.Instance.ServingStations.Count; ++i)
        {
            GameObject station = TycoonManager.Instance.ServingStations[i];
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
            // TODO: Rotation 고정?
            _holdingFood.GetComponent<Collider>().enabled = true;
            _holdingFood.transform.SetParent(foodPlace.transform);
            _holdingFood.transform.localPosition = Vector3.zero;
            
            foodPlace.CurrentFood = _holdingFood.GetComponent<CookedFood>();
            _holdingFood = null;
        }
    }

    private void CleaningFood()
    {
        // TODO: Player Clean Anim, Player position 고정
        int lastIndex = _canCleaningFoods.Count - 1;
        StartCoroutine(CleanFood(_canCleaningFoods[lastIndex]));
    }

    private void ThrowAwayFood()
    {
        Destroy(_holdingFood.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.CookedFood))
        {
            CookedFood cookedFood = other.gameObject.GetComponent<CookedFood>();
            if (cookedFood != null)
            {
                if (cookedFood.ShouldClean)
                {
                    if (!_canCleaningFoods.Contains(other.gameObject))
                    {
                        _canCleaningFoods.Add(other.gameObject);
                    }
                }
                else if (cookedFood.CanHold && _holdingFood == null)
                {
                    _canHoldFood = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.CookedFood))
        {
            CookedFood cookedFood = other.gameObject.GetComponent<CookedFood>();
            if (cookedFood != null)
            {
                if (cookedFood.ShouldClean)
                {
                    if (_canCleaningFoods.Contains(other.gameObject))
                    {
                        _canCleaningFoods.Remove(other.gameObject);
                    }
                }
                else if (_canHoldFood == other.gameObject)
                {
                    _canHoldFood = null;
                }
            }
        }
    }

    #region Coroutine

    IEnumerator CleanFood(GameObject food)
    {
        _canCleaningFoods.Remove(food);
        food.GetComponent<CookedFood>().CurrentFoodPlace = null;

        Destroy(food);

        yield break;
        //yield return _waitCleaningTime;
    }

    #endregion
}
