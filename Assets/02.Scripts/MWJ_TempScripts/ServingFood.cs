using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;
    [SerializeField] private List<Transform> _servingStations;

    private GameObject _canHoldFood;
    private GameObject _HoldingFood;
    private bool _isHold = false;
    private const float _minDistanceToPutFood = 1.3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

        _HoldingFood = _canHoldFood;
        _HoldingFood.transform.position = _handTransform.position;
        _HoldingFood.transform.SetParent(_handTransform);
        _isHold = true;
    }

    private void PutdownFood()
    {
        float minDistance = Mathf.Infinity;
        Transform targetTransform = null;

        foreach (Transform t in _servingStations)
        {
            float d = Vector3.Distance(_handTransform.position, t.position);
            if (d < minDistance && d < _minDistanceToPutFood)
            {
                minDistance = d;
                targetTransform = t;
            }
        }

        if (targetTransform != null)
        {
            _HoldingFood.transform.position = targetTransform.position;
            _HoldingFood.transform.SetParent(null);
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
