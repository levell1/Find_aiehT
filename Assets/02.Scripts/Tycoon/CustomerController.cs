using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    #region Field
    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;

    private NavMeshAgent _agent;
    private Animator _animator;
    private CookedFood _targetFood;

    private float _waitTime = 10f;
    private bool _isGetFood = false;
    private bool _isOrderFood = false;

    private FoodPlace _targetFoodPlace;
    public FoodPlace TargetFoodPlace
    {
        get { return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            _targetFoodPlace.OnCustomerGetFood += GetFood;
        }
    }

    public CookedFood TargetFood
    {
        get { return _targetFood; }
        set { _targetFood = value; }
    }

    public Transform AgentDestination
    {
        set { _agent.SetDestination(value.position); }
    }

    public int AgentPriority
    {
        set { _agent.avoidancePriority = value; }
    }

    private Coroutine _co;

    #endregion

    #region Event

    public event Action<GameObject> OnCreateFood;

    #endregion

    private void OnEnable()
    {
        _tycoonManager = GameManager.instance.TycoonManager;
        _foodCreater = _tycoonManager._FoodCreater;
        _agent = GetComponent<NavMeshAgent>();

        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("IsWalk", true);
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            _animator.SetBool("IsWalk", false);
            transform.rotation = Quaternion.identity;

            if (!_isOrderFood)
            {
                SelectFood();
                _isOrderFood = true;
            }

            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                NoReceivedFood();
            }

            if (_isGetFood)
            {
                _isGetFood = false;
                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
        }
    }

    private void SelectFood()
    {
        List<GameObject> foodPrefabs = _tycoonManager.CustomerTargetFoodPrefabs;

        int targetFoodNum = UnityEngine.Random.Range(0, foodPrefabs.Count);
        _targetFood = foodPrefabs[targetFoodNum].GetComponent<CookedFood>();
        OnCreateFood?.Invoke(foodPrefabs[targetFoodNum]);
    }

    private void GetFood()
    {
        _targetFoodPlace.OnCustomerGetFood -= GetFood;

        if (_co == null)
        {
            _animator.SetTrigger("GetFood");
            _co = StartCoroutine(ExitRestaurant());
        }
    }

    private void NoReceivedFood()
    {
        //_targetFood = null;
        _targetFoodPlace.CurrentCustomer = null;
        _targetFoodPlace.OnCustomerGetFood -= GetFood;

        if (_co == null)
        {
            _animator.SetTrigger("Angry");
            _co = StartCoroutine(ExitRestaurant());
        }
    }

    #region Coroutine

    IEnumerator ExitRestaurant()
    {
        yield return new WaitForSeconds(2.5f);

        _agent.SetDestination(_tycoonManager.CreateCustomerPos.position);
        _animator.SetBool("IsWalk", true);

        _isGetFood = true;
        _isOrderFood = false;
        _waitTime = 3f;

        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _co = null;
    }

    #endregion
}
