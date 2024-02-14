using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    private Queue<FoodSO> _foodQueue = new();

    [SerializeField] private List<GameObject> _createStations;

    [SerializeField] private float _foodCreateDelayTime;
    public float FoodCreateDelayTime
    {
        get { return _foodCreateDelayTime; }
        set { _foodCreateDelayTime = value; }
    }

    private Coroutine _co = null;

    public void SubscribeCreateFoodEvent(CustomerController customer)
    {
        customer.OnCreateFood += StartCreateFood;
    }

    public void UnsubscribeCreateFoodEvent(CustomerController customer)
    {
        customer.OnCreateFood -= StartCreateFood;
    }

    private void StartCreateFood(FoodSO obj)
    {
        _foodQueue.Enqueue(obj);
        if (_co == null)
            _co = StartCoroutine(MakeFood());
    }

    IEnumerator MakeFood()
    {
        while (_foodQueue.Count > 0)
        {
            // yield return new WaitForSeconds(_foodCreateDelayTime);
            for (int i = 0; i < _createStations.Count; ++i)
            {
                CookedFood currentFood = _createStations[i].GetComponent<FoodPlace>().CurrentFood;
                if (currentFood == null)
                {
                    FoodSO currentFoodSO = _foodQueue.Dequeue();
                    TycoonManager.Instance.CookingUI.StartCooking(currentFoodSO);

                    yield return new WaitForSeconds(_foodCreateDelayTime);

                    GameObject food = Instantiate(currentFoodSO.CookedFoodObject, _createStations[i].transform);
                    currentFood = food.GetComponent<CookedFood>();

                    break;
                }
            }
        }
        _co = null;
        yield break;
    }
}
