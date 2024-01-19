using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TycoonManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _customerPrefabs;
    [SerializeField] public List<GameObject> CustomerTargetFoodPrefabs;

    public List<GameObject> ServingStations = new();
    [SerializeField] public List<GameObject> _destinations;
    [SerializeField] public Transform CreateCustomerPos;

    [SerializeField] private int _maxCustomerNum = 4;
    [SerializeField] private float _customerSpawnTime = 1.0f;

    [SerializeField] private int _todayMaxCustomerNum = 6;
    [SerializeField] private int _currentCustomerNum = 0;

    private List<bool> _isCustomerSitting = new();
    private List<(GameObject destination, int index)> availableDestinations = new();

    private Coroutine _co = null;
    
    private int _agentPriority = 0;


    public event Action<GameObject> OnCreateFood;
    
    private void Start()
    {
        for (int i = 0; i < _destinations.Count; ++i)
        {
            _isCustomerSitting.Add(false);
            _destinations[i].GetComponentInParent<FoodPlace>().SeatNum = i;
            ServingStations.Add(_destinations[i].transform.parent.gameObject);
        }
    }

    private void Update()
    {
        // TODO: Customer가 나갔을 때, coroutine이나 함수 실행으로 변경
        if (_currentCustomerNum < _maxCustomerNum
            && _todayMaxCustomerNum > 0
            && _co == null)
        {
            _co = StartCoroutine(CreateCustomerCoroutine());
        }
    }

    public void CustomerExit(int seatNum)
    {
        _isCustomerSitting[seatNum] = false;
        --_currentCustomerNum;
    }

    IEnumerator CreateCustomerCoroutine()
    {
        while (_currentCustomerNum < _maxCustomerNum && _todayMaxCustomerNum > 0)
        {
            yield return new WaitForSeconds(_customerSpawnTime);

            // TODO: customer 쪽에서 해줘야 하나?
            availableDestinations = _destinations
            .Select((d, i) => (d, i))
            .Where(tuple => !_isCustomerSitting[tuple.i])
            .ToList();

            if (availableDestinations.Count <= 0)
                yield break;

            // TODO: 고정된 string값("Customer") 처리
            GameObject customerObject = GameManager.instance.PoolingManager.GetObject("Customer");
            CustomerController customerController = customerObject.GetComponent<CustomerController>();

            int seatNum = UnityEngine.Random.Range(0, availableDestinations.Count);
            customerController.AgentDestination = availableDestinations[seatNum].destination.transform;

            //TODO: 목적지에 도착했을 때 정하는 것으로 변경
            int targetFoodNum = UnityEngine.Random.Range(0, CustomerTargetFoodPrefabs.Count);
            customerController.TargetFood = CustomerTargetFoodPrefabs[targetFoodNum];

            OnCreateFood?.Invoke(CustomerTargetFoodPrefabs[targetFoodNum]);
            

            int currentDestinationIndex = availableDestinations[seatNum].index;

            FoodPlace foodPlace = _destinations[currentDestinationIndex].GetComponentInParent<FoodPlace>(); 
            foodPlace.CurrentCustomer = customerController;
            customerController.TargetFoodPlace = foodPlace;

            ++_agentPriority;
            customerController.AgentPriority = _agentPriority;

            _isCustomerSitting[currentDestinationIndex] = true;
            ++_currentCustomerNum;
            --_todayMaxCustomerNum;
        }
        _co = null;
    }
}
