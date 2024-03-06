using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TycoonManager : MonoSingleton<TycoonManager>
{
    #region Field

    public TycoonUI _TycoonUI;
    public FoodCreater _FoodCreater;
    public CookingUI CookingUI;
    public CinemachineVirtualCamera TycoonVirtualCamera;
    public Transform CustomerCreatePos;
    private GameObject _playerInteraction;

    [SerializeField] private List<GameObject> _destinations;
    private List<(GameObject destination, int index)> _availableDestinations = new();

    [SerializeField] private GameObject _prepareStation;
    [SerializeField] private GameObject _exitStation;
    public List<GameObject> ServingStations;
    private List<OrderFood> _todayFoods = new();
    private List<bool> _isCustomerSitting = new();

    [SerializeField] private float _customerEatTime;
    [SerializeField] private float _customerSpawnTime;
    public float CustomerWaitTime;

    [SerializeField] private int _maxCustomerNum;
    [SerializeField] private int _currentCustomerNum;   //TODO: SerializeField 제거
    [SerializeField] private int _todayMaxCustomerNum;
    public int AngryCustomerNum = 0;
    public int AgentPriority = 0;
    private int _tycoonMainQuest = 30001;

    private WaitForSeconds _waitForCustomerSpawnTime;
    public WaitForSeconds _waitForCustomerEatTime;

    #endregion

    #region Property

    public List<OrderFood> TodayFoods { get { return _todayFoods; } }

    public int TodayMaxCustomerNum 
    {
        get { return _todayMaxCustomerNum; }
        set { _todayMaxCustomerNum = value; }
    }

    #endregion

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
        _waitForCustomerSpawnTime = new WaitForSeconds(_customerSpawnTime);
        _waitForCustomerEatTime = new WaitForSeconds(_customerEatTime);

        DecideTodayCustomerNum();
    }

    public void TycoonGameStart()
    {
        DecideTodayFoods();

        _playerInteraction.SetActive(false);
        GameManager.Instance.CameraManager.TycoonCamSetting();
        StartCoroutine(CreateCustomerCoroutine());
        _TycoonUI.UpdateInitUI();

        _prepareStation.GetComponent<PrepareStation>().OffPrepareStation();
        _exitStation.GetComponent<Collider>().enabled = false;
        _TycoonUI.ShowTycoonStartText();

        GameManager.Instance.Player.GetComponent<ServingFood>().IsTycoonGameOver = false;
    }

    private void TycoonGameEnd()
    {
        _TycoonUI.OnReusltUI();

        QuestManager questManager = GameManager.Instance.QuestManager;

        if (GameManager.Instance.GlobalTimeManager.Day >=1)
        {
            Quest quest = questManager.ActiveMainQuests[0];

            if (!quest.IsProgress && AngryCustomerNum == 0)
            {
                questManager.UpdateMainQuest(_tycoonMainQuest);
            }
        }
        
        GameManager.Instance.DataManager.RemoveOrderData();
       
        _playerInteraction.SetActive(true);
    }

    private void DecideTodayCustomerNum()
    {
        int playerLevel = GameManager.Instance.Player.GetComponent<Player>().Data.PlayerData.PlayerLevel;
        _todayMaxCustomerNum = 5 + (playerLevel - 1) * 2;
    }

    private void DecideTodayFoods()
    {
        _todayFoods = GameManager.Instance.DataManager.Orders;
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

    public int DecideCustomerDestination(Customer customer)
    {
        _availableDestinations = _destinations
                .Select((d, i) => (d, i))
                .Where(tuple => !_isCustomerSitting[tuple.i])
                .ToList();

        int seatNum = UnityEngine.Random.Range(0, _availableDestinations.Count);
        customer.AgentDestination = _availableDestinations[seatNum].destination.transform;

        return _availableDestinations[seatNum].index;
    }

    public void SetCustomerFoodPlace(Customer customer,int seatIndex)
    {
        FoodPlace foodPlace = _destinations[seatIndex].GetComponentInParent<FoodPlace>();
        foodPlace.CurrentCustomer = customer;
        customer.TargetFoodPlace = foodPlace;

        _isCustomerSitting[seatIndex] = true;
    }

    IEnumerator CreateCustomerCoroutine()
    {
        while (_todayMaxCustomerNum > 0)
        {
            yield return _waitForCustomerSpawnTime;

            if (_currentCustomerNum < _maxCustomerNum)
            {
                GameManager.Instance.PoolingManager.GetObject(PoolingObjectName.Customer);

                ++_currentCustomerNum;
                --_todayMaxCustomerNum;

                _TycoonUI.UpdateRemainingCustomerNum();
            }
        }
    }
}
