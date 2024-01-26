using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    #region Field
    private TycoonManager _tycoonManager;
    private FoodCreater _foodCreater;
    private CookedFood _targetFood;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Collider _collider;

    private float _waitTime;
    private const float _agentBaseOffset = 0.45f;
    private bool _isGetFood = false;
    private bool _isOrderFood = false;

    List<GameObject> ais = new();

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
    public event Action OnCustomerExit;

    #endregion

    private void OnEnable()
    {
        _tycoonManager = TycoonManager.Instance;
        _foodCreater = _tycoonManager._FoodCreater;

        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _animator = GetComponentInChildren<Animator>();

        _animator.SetBool("IsWalk", true);
        _agent.baseOffset = 0.0f;
        _waitTime = _tycoonManager._customerWaitTime;
        _isOrderFood = false;

        ais.Clear();
    }

    private void Update()
    {
        if (!_agent.hasPath)
        {
            if (!_isOrderFood)
            {
                SelectFood();
                _isOrderFood = true;

                //TODO: 한번만 실행되도록 다른곳으로 이동..
                _animator.SetBool("IsWalk", false);
                _agent.baseOffset = _agentBaseOffset;
                transform.rotation = _targetFoodPlace.gameObject.transform.rotation;

                _collider.enabled = false;
                _agent.isStopped = true;
            }

            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                NoReceivedFood();
            }

            if (_isGetFood)
            {
                _isGetFood = false;

                OnCustomerExit?.Invoke();
                _targetFoodPlace.CurrentCustomer = null;
                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
        }

        //TODO
        ais.RemoveAll(ai => !ai.activeInHierarchy);
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
        if (_co == null)
        {
            _animator.SetTrigger("GetFood");

            _co = StartCoroutine(EatFood());
        }
    }

    private void NoReceivedFood()
    {
        if (_co == null)
        {
            _animator.SetTrigger("Angry");
            _co = StartCoroutine(ExitRestaurant());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (Mathf.Approximately(_agent.destination.x, _tycoonManager.CustomerCreatePos.position.x)
                && Mathf.Approximately(_agent.destination.z, _tycoonManager.CustomerCreatePos.position.z)
                && otherAgent.isStopped == false)
            {
                if (!ais.Contains(other.gameObject))
                {
                    ais.Add(other.gameObject);
                    _agent.isStopped = true;
                    _animator.SetBool("IsIdle", true);

                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (otherAgent.isStopped == true)
            {
                if (ais.Contains(other.gameObject))
                    ais.Remove(other.gameObject);

                if (ais.Count == 0)
                {
                    _agent.isStopped = false;
                    _animator.SetBool("IsIdle", false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            if (ais.Contains(other.gameObject))
                ais.Remove(other.gameObject);

            if (ais.Count == 0)
            {
                _agent.isStopped = false;
                _animator.SetBool("IsIdle", false);
            }
        }
    }


    #region Coroutine

    IEnumerator EatFood()
    {
        _animator.SetBool("IsEat", true);
        yield return new WaitForSeconds(10f);
        _animator.SetBool("IsEat", false);

        StartCoroutine(ExitRestaurant());
    }

    IEnumerator ExitRestaurant()
    {
        _targetFoodPlace.OnCustomerGetFood -= GetFood;

        yield return new WaitForSeconds(5f);

        _agent.SetDestination(_tycoonManager.CustomerCreatePos.position);
        _animator.SetBool("IsWalk", true);

        _agent.baseOffset = 0.0f;
        _isGetFood = true;


        _collider.enabled = true;
        _agent.isStopped = false;

        _waitTime = _tycoonManager._customerWaitTime;

        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _co = null;

        StopAllCoroutines();
    }
    #endregion
}
