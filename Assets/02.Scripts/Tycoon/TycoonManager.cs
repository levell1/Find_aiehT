using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TycoonManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _customerPrefabs;
    [SerializeField] private List<GameObject> _customerTargetFoodPrefabs;

    [SerializeField] private List<Transform> _destinations;
    [SerializeField] private Transform _createCustomerPos;

    [SerializeField] private int _maxCustomerNum = 4;
    [SerializeField] private float _customerSpawnTime = 1.0f;

    private int _currentCustomerNum = 0;
    private List<bool> _isCustomerSitting = new();

    private void Start()
    {
        for (int i = 0; i < _destinations.Count; ++i)
        {
            _isCustomerSitting.Add(false);
        }

        StartCoroutine(CreateCustomerCoroutine());
    }

    // TODO: _currentCustomerNum < _maxCustomerNum 일 경우 (나갔을 경우) startCoroutine
    // 아니면 함수로 만들기
    IEnumerator CreateCustomerCoroutine()
    {
        while (_currentCustomerNum < _maxCustomerNum)
        {
            // TODO: Object Pool
            // TODO: customer 쪽에서 해줘야 하나?
            List<(Transform destination, int index)> availableDestinations = _destinations
            .Select((d, i) => (d, i))
            .Where(tuple => !_isCustomerSitting[tuple.i])
            .ToList();
            
            if (availableDestinations.Count <= 0)
                yield break;

            int customerTypeNum = Random.Range(0, _customerPrefabs.Count);
            GameObject customerObject = Instantiate(_customerPrefabs[customerTypeNum], _createCustomerPos);

            CustomerController npcController = customerObject.GetComponent<CustomerController>();

            int seatNum = Random.Range(0, availableDestinations.Count);
            npcController.AgentDestination = availableDestinations[seatNum].destination;

            int targetFoodNum = Random.Range(0, _customerTargetFoodPrefabs.Count);
            _destinations[seatNum].gameObject.GetComponentInParent<FoodPlace>().CurrentCustomer = npcController;
            npcController.TargetFood = _customerTargetFoodPrefabs[targetFoodNum];

            _isCustomerSitting[availableDestinations[seatNum].index] = true;
            ++_currentCustomerNum;

            yield return new WaitForSeconds(_customerSpawnTime);
        }
    }
}
