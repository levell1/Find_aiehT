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
    private int _foodNumInStation = 0;

    private bool _canMakeFood = true;
    public bool CanMakeFood { get { return _canMakeFood; } }

    private Coroutine _co = null;

    private void Start()
    {
        GameManager.Instance.Player.GetComponent<ServingFood>().OnCheckCreateStation += Check;
    }

    private void Check()
    {
        if (_canMakeFood) return;

        --_foodNumInStation;
        _canMakeFood = true;

        if (_co == null)
            _co = StartCoroutine(MakeFood());
    }

    public void StartCreateFood(FoodSO obj)
    {
        _foodQueue.Enqueue(obj);

        if (_co == null)
            _co = StartCoroutine(MakeFood());
    }

    public IEnumerator MakeFood()
    {
        while (_foodQueue.Count > 0)
        {
            _foodNumInStation = 0;
            for (int i = 0; i < _createStations.Count; ++i)
            {
                CookedFood currentFood = _createStations[i].GetComponent<FoodPlace>().CurrentFood;
                if (currentFood == null)
                {
                    FoodSO currentFoodSO = _foodQueue.Dequeue();

                    yield return new WaitForSeconds(_foodCreateDelayTime);

                    GameObject food = Instantiate(currentFoodSO.CookedFoodObject, _createStations[i].transform);
                    _createStations[i].GetComponent<FoodPlace>().CurrentFood = food.GetComponent<CookedFood>();

                    break;
                }
                ++_foodNumInStation;
            }

            if (_foodNumInStation == _createStations.Count)
            {
                _canMakeFood = false;
                break;
            }
        }
        _co = null;
        yield break;
    }
}
