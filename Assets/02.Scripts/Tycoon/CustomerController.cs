using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CustomerController : MonoBehaviour
{
    #region Field
    private OrderFoodUI _orderFoodCanvas;

    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Collider _collider;

    private float _waitTime;
    private const float _agentBaseOffset = 0.57f;
    private bool _isExit = false;
    private bool _isSit = false;
    private bool _isGetFood = false;

    private List<GameObject> _collidingAIs = new();

    private FoodPlace _targetFoodPlace;
    private string _targetFoodName;

    private WaitForSeconds _angryAnimationTime = new WaitForSeconds(2f);
    private WaitForSeconds _standAnimationTime = new WaitForSeconds(4f);

    #endregion

    #region Property
    public FoodPlace TargetFoodPlace
    {
        get { return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            _targetFoodPlace.OnCustomerGetFood += GetFood;
        }
    }

    public string TargetFoodName
    {
        get { return _targetFoodName; }
        set { _targetFoodName = value; }
    }

    public Transform AgentDestination
    {
        set { _agent.SetDestination(value.position); }
    }

    public int AgentPriority
    {
        set { _agent.avoidancePriority = value; }
    }

    #endregion

    #region Event

    public event Action<FoodSO> OnCreateFood;
    public event Action OnCustomerExit;
    public event Action OnSelectFood;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _orderFoodCanvas = GetComponentInChildren<OrderFoodUI>();

        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _animator = GetComponentInChildren<Animator>();
    }


    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == SceneName.TycoonScene)
        {
            _tycoonManager = TycoonManager.Instance;
            _foodCreater = _tycoonManager._FoodCreater;
            _waitTime = _tycoonManager.CustomerWaitTime;
        }
        Init();
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            if (!_isSit)
            {
                SelectFood();
                _isSit = true;

                _animator.SetBool(AnimationParameterName.TycoonIsWalk, false);
                _agent.baseOffset = _agentBaseOffset;
                transform.rotation = _targetFoodPlace.gameObject.transform.rotation;

                _collider.enabled = false;
                _agent.isStopped = true;
            }
            else if(!_isGetFood)
            {
                _waitTime -= Time.deltaTime;
                if (_waitTime <= 0)
                    StartCoroutine(NoReceivedFood());

                _orderFoodCanvas.BalloonBack.fillAmount = _waitTime / _tycoonManager.CustomerWaitTime;
            }

            if (_isExit)
            {
                _isExit = false;

                OnCustomerExit?.Invoke();

                _targetFoodPlace.OnCustomerGetFood -= GetFood;
                _targetFoodPlace.CurrentCustomer = null;
                _targetFoodPlace = null;
                GameManager.Instance.PoolingManager.ReturnObject(gameObject);
            }
        }
        else
        {
            //TODO
            _collidingAIs.RemoveAll(ai => !ai.activeSelf || ai.GetComponent<NavMeshAgent>().isStopped);
            CheckCollidingAICount();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.AI))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (Mathf.Approximately(_agent.destination.x, _tycoonManager.CustomerCreatePos.position.x)
                && Mathf.Approximately(_agent.destination.z, _tycoonManager.CustomerCreatePos.position.z)
                && otherAgent.isStopped == false
                && !_collidingAIs.Contains(other.gameObject))
            {
                _collidingAIs.Add(other.gameObject);
                _agent.isStopped = true;
                _animator.SetBool(AnimationParameterName.TycoonIsIdle, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.AI))
        {
            RemoveList(other.gameObject);
        }
    }

    #endregion

    #region
    private void Init()
    {
        _animator.SetBool(AnimationParameterName.TycoonIsWalk, true);

        _isSit = false;
        _isGetFood = false;

        transform.rotation = Quaternion.identity;
        _animator.gameObject.transform.localPosition = Vector3.zero;
        _animator.gameObject.transform.localRotation = Quaternion.identity;

        _collidingAIs.Clear();
    }

    private void SelectFood()
    {
        List<OrderFood> menu = _tycoonManager.TodayFoods;

        int targetFoodNum = UnityEngine.Random.Range(0, menu.Count);
        _targetFoodName = menu[targetFoodNum].FoodSO.CookedFoodObject.name;

        _orderFoodCanvas.SelectFood.sprite = menu[targetFoodNum].FoodSO.FoodSprite;
        _orderFoodCanvas.ActiveUI();

        OnCreateFood?.Invoke(menu[targetFoodNum].FoodSO);
        OnSelectFood?.Invoke();

        _tycoonManager.CookingUI.StartCooking(menu[targetFoodNum].FoodSO);

        --menu[targetFoodNum].FoodCount;
        if (menu[targetFoodNum].FoodCount == 0)
            menu.Remove(menu[targetFoodNum]);
    }

    private void RemoveList(GameObject obj)
    {
        if (_collidingAIs.Contains(obj))
        {
            _collidingAIs.Remove(obj);
        }

        CheckCollidingAICount();
    }

    private void CheckCollidingAICount()
    {
        if (_collidingAIs.Count == 0)
        {
            _agent.isStopped = false;
            _animator.SetBool(AnimationParameterName.TycoonIsIdle, false);
        }
    }

    private void GetFood()
    {
        _isGetFood = true;
        
        _orderFoodCanvas.InactiveUI();
        _animator.SetTrigger(AnimationParameterName.TycoonGetFood);

        GetComponentInChildren<CustomerEffect>().PlayGetCoinEffect();
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Money,Vector3.zero, 0.5f);
        StartCoroutine(EatFood());
    }

    #endregion

    #region Coroutine

    IEnumerator EatFood()
    {
        _targetFoodName = null;
        _waitTime = _tycoonManager.CustomerWaitTime;

        _animator.SetBool(AnimationParameterName.TycoonIsEat, true);
        int random = UnityEngine.Random.Range(1, 3);
        
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Eat+random.ToString(), Vector3.zero, 1f);

        yield return TycoonManager.Instance._waitForCustomerEatTime;
        _animator.SetBool(AnimationParameterName.TycoonIsEat, false);

        StartCoroutine(ExitRestaurant());
    }
    
    IEnumerator NoReceivedFood()
    {
        _isGetFood = true;
        _targetFoodName = null;
        _waitTime = _tycoonManager.CustomerWaitTime;

        _orderFoodCanvas.InactiveUI();

        ++_tycoonManager.AngryCustomerNum;

        _animator.SetTrigger(AnimationParameterName.TycoonAngry);
        yield return _angryAnimationTime;

        StartCoroutine(ExitRestaurant());
    }

    IEnumerator ExitRestaurant()
    {
        yield return _standAnimationTime;

        _agent.SetDestination(_tycoonManager.CustomerCreatePos.position);
        _animator.SetBool(AnimationParameterName.TycoonIsWalk, true);
        _agent.baseOffset = 0.0f;

        // another AI
        _collider.enabled = true;
        _agent.isStopped = false;

        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _isExit = true;
    }

    #endregion
}
