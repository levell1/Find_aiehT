using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class CustomerController : MonoBehaviour
{
    #region Field
    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;

    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _targetFood;

    private float _waitTime = 20f;
    private bool _isGetFood = false;
    private bool _isOrderFood = false;

    private FoodPlace _targetFoodPlace;
    public FoodPlace TargetFoodPlace
    {
        get {  return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            _targetFoodPlace.OnCustomerGetFood += GetFood;
        }
    }

    public GameObject TargetFood
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
        if(!_agent.hasPath)
        {
            _animator.SetBool("IsWalk", false);
            transform.rotation = Quaternion.identity;

            if (!_isOrderFood)
            {
                SelectFood();
            }
            else if(_waitTime > 0)
            {
                _waitTime -= Time.deltaTime;
            }

            if (_isGetFood)
            {
                _isGetFood = false;
                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
            else if (_waitTime <= 0)
            {
                NoReceivedFood();
            }
        }
    }

    private void SelectFood()
    {
        List<GameObject> foodPrefabs = _tycoonManager.CustomerTargetFoodPrefabs;
        
        int targetFoodNum = UnityEngine.Random.Range(0, foodPrefabs.Count);
        _targetFood = foodPrefabs[targetFoodNum];
        OnCreateFood?.Invoke(foodPrefabs[targetFoodNum]);

        _isOrderFood = true;
    }

    private void GetFood()
    {
        _animator.SetTrigger("GetFood");
        if (_co == null)
        {
            _co = StartCoroutine(ExitRestaurant());
        }
    }

    private void NoReceivedFood()
    {
        _animator.SetTrigger("Angry");
        if (_co == null)
        {
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

        _targetFoodPlace.OnCustomerGetFood -= GetFood;
        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _co = null;
    }

    #endregion
}
