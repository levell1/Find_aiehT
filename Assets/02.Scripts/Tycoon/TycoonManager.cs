using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TycoonManager : MonoBehaviour
{
    [SerializeField] public FoodCreater _FoodCreater;
    [SerializeField] public List<GameObject> CustomerTargetFoodPrefabs;

    public List<GameObject> ServingStations = new();    // Add CreateStations In Inspector
    [SerializeField] private List<GameObject> _destinations;
    [SerializeField] public Transform CreateCustomerPos;

    [SerializeField] private int _maxCustomerNum = 4;
    [SerializeField] private float _customerSpawnTime;
    [SerializeField] public float _customerWaitTime;

    [SerializeField] private int _todayMaxCustomerNum = 6;
    [SerializeField] private int _currentCustomerNum = 0;

    private List<bool> _isCustomerSitting = new();
    private List<(GameObject destination, int index)> availableDestinations = new();

    private Coroutine _co = null;
    
    private int _agentPriority = 0;
    private bool _isStart = false;

    private static TycoonManager _instance;

    public static TycoonManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<TycoonManager>();
                if(_instance == null)
                {
                    GameObject obj = new GameObject("TycoonManager");
                    _instance = obj.AddComponent<TycoonManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }

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
        // TODO: 타이쿤 게임이 시작됐을 때
        //if (_isStart)
        {
            if (_currentCustomerNum < _maxCustomerNum
                && _todayMaxCustomerNum > 0
                && _co == null)
            {
                _co = StartCoroutine(CreateCustomerCoroutine());
            }
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
            // 손님이 없는 자리만 List로
            // TODO: customer 쪽에서 해줘야 하나?
            availableDestinations = _destinations
            .Select((d, i) => (d, i))
            .Where(tuple => !_isCustomerSitting[tuple.i])
            .ToList();

            if (availableDestinations.Count <= 0)
                yield break;

            // 손님 생성
            // TODO: 고정된 string값("Customer") 처리
            GameObject customerObject = GameManager.instance.PoolingManager.GetObject("Customer");
            CustomerController customerController = customerObject.GetComponent<CustomerController>();

            // 손님 자리 배치
            int seatNum = UnityEngine.Random.Range(0, availableDestinations.Count);
            customerController.AgentDestination = availableDestinations[seatNum].destination.transform;

            // 손님 요청 음식 결정
            // TODO: SO Data로 변경
            _FoodCreater.SubscribeCreateFoodEvent(customerController);


            int currentDestinationIndex = availableDestinations[seatNum].index;

            // 손님의 자리(FoodPlace)에 손님 지정, 손님의 스크립트에 자리 지정 (이벤트를 위해)
            FoodPlace foodPlace = _destinations[currentDestinationIndex].GetComponentInParent<FoodPlace>(); 
            foodPlace.CurrentCustomer = customerController;
            customerController.TargetFoodPlace = foodPlace;

            // AI 우선순위 지정
            ++_agentPriority;
            customerController.AgentPriority = _agentPriority;

            // 자리가 있음을 알려주는 bool값 true
            _isCustomerSitting[currentDestinationIndex] = true;

            // 현재 손님 수 ++, 오늘 올 손님 --
            ++_currentCustomerNum;
            --_todayMaxCustomerNum;


            yield return new WaitForSeconds(_customerSpawnTime);
        }
        _co = null;
    }
}
