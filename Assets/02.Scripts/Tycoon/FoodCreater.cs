using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    private Queue<GameObject> _foodQueue = new();

    [SerializeField] private List<GameObject> _createStations;

    private float _foodCreateDelayTime = 5f;
    private Coroutine _co;

    public void SubscribeCreateFoodEvent(CustomerController customer)
    {
        customer.OnCreateFood += StartCreateFood;
    }

    public void UnsubscribeCreateFoodEvent(CustomerController customer)
    {
        customer.OnCreateFood -= StartCreateFood;
    }

    private void StartCreateFood(GameObject obj)
    {
        _foodQueue.Enqueue(obj);

        if (_co == null)
        {
            _co = StartCoroutine(MakeFood());
        }
    }

    IEnumerator MakeFood()
    { 
        while(_foodQueue.Count > 0)
        {
            yield return new WaitForSeconds(_foodCreateDelayTime);

            for (int i = 0; i < _createStations.Count; ++i)
            {
                FoodPlace foodPlace = _createStations[i].GetComponent<FoodPlace>();
                if (foodPlace.CurrentFood == null)
                {
                    GameObject food = Instantiate(_foodQueue.Dequeue(), _createStations[i].transform);
                    foodPlace.CurrentFood = food.GetComponent<CookedFood>();
                    break;
                }
            }
        }
        
        _co = null;
        yield break;
    }
}
