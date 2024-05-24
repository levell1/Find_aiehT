using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Customer : MonoBehaviour
{
    #region Field

    [HideInInspector] public CustomerStateMachine StateMachine;
    [HideInInspector] public OrderFoodUI OrderFoodCanvas;
    [HideInInspector] public NavMeshAgent CustomerAgent;
    [HideInInspector] public Animator CustomerAnimator;
    [HideInInspector] public Collider CustomerCollider;

    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;
    private FoodPlace _targetFoodPlace;

    [HideInInspector] public float WaitTime;
    private bool _isSit = false;
    private bool _isGetFood = false;
    private bool _isExit = false;

    private List<GameObject> _collidingAIs = new();

    private WaitForSeconds _angryAnimationTime = new WaitForSeconds(2f);
    private WaitForSeconds _standAnimationTime = new WaitForSeconds(4f);

    #endregion

    #region Property
    public FoodPlace TargetFoodPlace
    {
        get { return _targetFoodPlace; }
        set
        {
            if (value == null)
            {
                _targetFoodPlace.OnCustomerGetFood -= GetFood;
                _targetFoodPlace.CurrentCustomer = null;
            }
            else
                value.OnCustomerGetFood += GetFood;

            _targetFoodPlace = value;
        }
    }

    public FoodSO TargetFood { get; set; } = null;

    public Transform AgentDestination
    {
        set { CustomerAgent.SetDestination(value.position); }
    }

    public int AgentPriority
    {
        set { CustomerAgent.avoidancePriority = value; }
    }

    #endregion

    #region Event

    public event Action OnSelectFood;
    public event Action OnCustomerExit;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == SceneName.TycoonScene)
        {
            _tycoonManager = TycoonManager.Instance;
            _foodCreater = _tycoonManager._FoodCreater;
            
            _isSit = false;
            _isGetFood = false;
            _isExit = false;

            _collidingAIs.Clear();

            StateMachine = new CustomerStateMachine(this);
            StateMachine.ChangeState(StateMachine.EnterState);
        }
    }

    private void Awake()
    {
        CustomerAgent = GetComponent<NavMeshAgent>();
        CustomerCollider = GetComponent<Collider>();
        CustomerAnimator = GetComponentInChildren<Animator>();
        OrderFoodCanvas = GetComponentInChildren<OrderFoodUI>();
    }

    private void Update()
    {
        if (!CustomerAgent.hasPath)
        {
            if (!_isSit)
            {
                _isSit = true;

                StateMachine.ChangeState(StateMachine.WaitingState);
                _foodCreater.StartCreateFood(TargetFood);
                OnSelectFood?.Invoke();
            }
            else if (!_isGetFood)
            {
                WaitTime -= Time.deltaTime;
                if (WaitTime <= 0)
                {
                    _isGetFood = true;
                    StartCoroutine(NoReceivedFood());
                }
                OrderFoodCanvas.BalloonBack.fillAmount = WaitTime / _tycoonManager.CustomerWaitTime;
            }

            if (_isExit)
            {
                OnCustomerExit?.Invoke();
                StateMachine.ExitingState.Exit();
            }
        }
        else
        {
            _collidingAIs.RemoveAll(ai => !ai.activeSelf || ai.GetComponent<NavMeshAgent>().isStopped);
            CheckCollidingAICount();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.AI))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (Mathf.Approximately(CustomerAgent.destination.x, _tycoonManager.CustomerCreatePos.position.x)
                && Mathf.Approximately(CustomerAgent.destination.z, _tycoonManager.CustomerCreatePos.position.z)
                && otherAgent.isStopped == false
                && !_collidingAIs.Contains(other.gameObject))
            {
                _collidingAIs.Add(other.gameObject);
                CustomerAgent.isStopped = true;
                CustomerAnimator.SetBool(AnimationParameterName.TycoonIsIdle, true);
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
            CustomerAgent.isStopped = false;
            CustomerAnimator.SetBool(AnimationParameterName.TycoonIsIdle, false);
        }
    }

    public void GetFood()
    {
        _isGetFood = true;
        StartCoroutine(EatFood());
    }

    #endregion

    #region Coroutine

    IEnumerator EatFood()
    {
        StateMachine.ChangeState(StateMachine.EatingState);

        yield return TycoonManager.Instance._waitForCustomerEatTime;

        StartCoroutine(ExitRestaurant());
    }

    IEnumerator NoReceivedFood()
    {
        StateMachine.ChangeState(StateMachine.AngryState);
        
        yield return _angryAnimationTime;

        StartCoroutine(ExitRestaurant());
    }

    IEnumerator ExitRestaurant()
    {
        CustomerAnimator.SetBool(AnimationParameterName.TycoonIsEat, false);
        TargetFoodPlace = null;

        yield return _standAnimationTime;
        StateMachine.ChangeState(StateMachine.ExitingState);

        _isExit = true;
    }

    #endregion
}
