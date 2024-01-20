using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    private TycoonManager _tycoonManager;
    private Queue<GameObject> _foodQueue = new();

    [SerializeField] private List<GameObject> _createStations;

    private float _foodCreateDelayTime = 3f;
    private Coroutine _co;

    private void Start()
    {
        _tycoonManager = GameManager.instance.TycoonManager;
        _tycoonManager.OnCreateFood += StartCreateFood;
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
            for(int i = 0; i < _createStations.Count; ++i)
            {
                FoodPlace foodPlace = _createStations[i].GetComponent<FoodPlace>();
                if (foodPlace.CurrentFood == null)
                {
                    GameObject food = Instantiate(_foodQueue.Dequeue(), _createStations[i].transform);
                    foodPlace.CurrentFood = food.GetComponent<CookedFood>();
                    break;
                }
            }

            yield return new WaitForSeconds(_foodCreateDelayTime);
        }
        
        _co = null;
        yield break;
    }
}
