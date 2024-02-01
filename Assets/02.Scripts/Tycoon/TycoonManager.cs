using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TycoonManager : MonoSingleton<TycoonManager>
{
    [SerializeField] public TycoonUI _TycoonUI;
    [SerializeField] public FoodCreater _FoodCreater;

    [SerializeField] private List<GameObject> _destinations;
    private List<(GameObject destination, int index)> availableDestinations = new();
    public List<GameObject> ServingStations = new();    // Add CreateStations In Inspector
    [NonSerialized] public List<OrderFood> TodayFoods = new();
    private List<bool> _isCustomerSitting = new();

    [SerializeField] public Transform CustomerCreatePos;
    private GameObject _playerInteraction;

    [SerializeField] private float _customerSpawnTime;
    [SerializeField] public float CustomerWaitTime;

    [SerializeField] private int _maxCustomerNum = 4;
    [SerializeField] private int _currentCustomerNum = 0;   //TODO: SerializeField 제거
    [SerializeField] private int _todayMaxCustomerNum;
    public int TodayMaxCustomerNum // TODO: 레벨 당 손님수 정하는 함수
    {
        get { return _todayMaxCustomerNum; }
        set { _todayMaxCustomerNum = value; }
    }

    private int _agentPriority = 0;

    WaitForSeconds waitForCustomerSpawnTime;

    private void Start()
    {
        for (int i = 0; i < _destinations.Count; ++i)
        {
            _isCustomerSitting.Add(false);
            _destinations[i].GetComponentInParent<FoodPlace>().SeatNum = i;

            //TODO: Inspector 창에서 넣어주기 vs 여기서 코드로 넣기
            ServingStations.Add(_destinations[i].transform.parent.gameObject);
        }

        _playerInteraction = GameManager.Instance.Player.GetComponentInChildren<PlayerInteraction>().gameObject;
        waitForCustomerSpawnTime = new WaitForSeconds(_customerSpawnTime);
    }

    public void TycoonGameStart()
    {
        DecideTodayFoods();
        _playerInteraction.SetActive(false);

        StartCoroutine(CreateCustomerCoroutine());
    }

    private void DecideTodayFoods()
    {
        TodayFoods = GameManager.Instance.DataManager.Orders;
        GameManager.Instance.DataManager.DecideBreadNum();
    }

    public void CustomerExit(int seatNum)
    {
        _isCustomerSitting[seatNum] = false;
        --_currentCustomerNum;

        if (_todayMaxCustomerNum == 0 && _currentCustomerNum == 0)
        {
            TycoonGameEnd();
        }
    }

    private void TycoonGameEnd()
    {
        _TycoonUI.OnReusltUI();
        _playerInteraction.SetActive(true);
    }

    IEnumerator CreateCustomerCoroutine()
    {
        while (_todayMaxCustomerNum > 0)
        {
            yield return waitForCustomerSpawnTime;

            if (_currentCustomerNum < _maxCustomerNum)
            {
                // 손님이 없는 자리만 List로
                availableDestinations = _destinations
                .Select((d, i) => (d, i))
                .Where(tuple => !_isCustomerSitting[tuple.i])
                .ToList();

                if (availableDestinations.Count <= 0)
                    continue;

                // 손님 생성
                GameObject customerObject = GameManager.Instance.PoolingManager.GetObject(PoolingObjectName.Customer);
                CustomerController customerController = customerObject.GetComponent<CustomerController>();

                // 손님 자리 배치
                int seatNum = UnityEngine.Random.Range(0, availableDestinations.Count);
                customerController.AgentDestination = availableDestinations[seatNum].destination.transform;

                // FoddCreater의 음식 제조 event를 위해 구독
                _FoodCreater.SubscribeCreateFoodEvent(customerController);

                int currentDestinationIndex = availableDestinations[seatNum].index;

                // 손님의 자리(FoodPlace)에 손님 지정, 손님의 스크립트에 자리 지정 (이벤트를 위해)
                FoodPlace foodPlace = _destinations[currentDestinationIndex].GetComponentInParent<FoodPlace>();
                foodPlace.CurrentCustomer = customerController;
                customerController.TargetFoodPlace = foodPlace;

                // AI 우선순위 지정
                customerController.AgentPriority = ++_agentPriority;

                // 자리가 있음을 알려주는 bool값 true
                _isCustomerSitting[currentDestinationIndex] = true;

                // 현재 손님 수 ++, 오늘 올 손님 --
                ++_currentCustomerNum;
                --_todayMaxCustomerNum;

                _TycoonUI.UpdateRemainingCustomerNum();
            }
        }
    }
}
