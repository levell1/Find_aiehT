using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCreater : MonoBehaviour
{
    private TycoonManager _tycoonManager;

    private Queue<GameObject> _foodQueue = new();

    // TODO: bool isFoodHere을 FoodPlace로
    private List<(Transform t, bool isFoodHere)> _createStations = new();

    [SerializeField] private List<Transform> _createPos;

    private Coroutine _co;

    private void Start()
    {
        for (int i = 0; i < _createPos.Count; ++i)
        {
            _createStations.Add((_createPos[i], false));
        }

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
            for(int i = 0; i< _createStations.Count; ++i)
            {
                if (!_createStations[i].Item2)
                {
                    GameObject food = Instantiate(_foodQueue.Dequeue(), _createStations[i].Item1);
                    _createStations[i] = (_createStations[i].Item1, true);
                }
            }

            yield return new WaitForSeconds(5f);
        }
        
        _co = null;

        yield break;
    }
}
