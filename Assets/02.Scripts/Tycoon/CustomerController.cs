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
    private bool _isExit = false;
    private bool _isOrderFood = false;

    private List<GameObject> _collidingAIs = new();

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

    #region MonoBehaviour

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _tycoonManager = TycoonManager.Instance;
        _foodCreater = _tycoonManager._FoodCreater;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            if(_agent.destination != _tycoonManager.CustomerCreatePos.position)
            {
            }

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

            if (_isExit)
            {
                _isExit = false;

                StopAllCoroutines();
                OnCustomerExit?.Invoke();
                _targetFoodPlace.CurrentCustomer = null;
                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
        }
        else
        {
            //TODO
            _collidingAIs.RemoveAll(ai => !ai.activeInHierarchy);
            foreach(GameObject ai in _collidingAIs)
            {
                if (!ai.GetComponent<NavMeshAgent>().isStopped)
                {
                    RemoveList(ai);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();

            if (Mathf.Approximately(_agent.destination.x, _tycoonManager.CustomerCreatePos.position.x)
                && Mathf.Approximately(_agent.destination.z, _tycoonManager.CustomerCreatePos.position.z)
                && otherAgent.isStopped == false
                && !_collidingAIs.Contains(other.gameObject))
            {
                _collidingAIs.Add(other.gameObject);
                _agent.isStopped = true;
                _animator.SetBool("IsIdle", true);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("AI"))
    //    {
    //        NavMeshAgent otherAgent = other.gameObject.GetComponent<NavMeshAgent>();
    //        if (otherAgent.isStopped == true)
    //        {
    //            RemoveList(other.gameObject);
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            RemoveList(other.gameObject);
        }
    }

    #endregion

    #region 
    private void Init()
    {
        _animator.SetBool("IsWalk", true);
        //_agent.baseOffset = 0.0f;
        //_waitTime = _tycoonManager.CustomerWaitTime;
        _isOrderFood = false;

        transform.rotation = Quaternion.identity;
        _animator.gameObject.transform.localPosition = Vector3.zero;
        _animator.gameObject.transform.localRotation = Quaternion.identity;

        _collidingAIs.Clear();
    }

    private void SelectFood()
    {
        List<GameObject> foodPrefabs = _tycoonManager.CustomerTargetFoodPrefabs;

        int targetFoodNum = UnityEngine.Random.Range(0, foodPrefabs.Count);
        _targetFood = foodPrefabs[targetFoodNum].GetComponent<CookedFood>();
        OnCreateFood?.Invoke(foodPrefabs[targetFoodNum]);
    }

    private void RemoveList(GameObject obj)
    {
        if (_collidingAIs.Contains(obj))
        {
            _collidingAIs.Remove(obj);
        }

        if (_collidingAIs.Count == 0)
        {
            _agent.isStopped = false;
            _animator.SetBool("IsIdle", false);
        }
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

    #endregion

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

        yield return new WaitForSeconds(8f);

        _agent.SetDestination(_tycoonManager.CustomerCreatePos.position);
        _animator.SetBool("IsWalk", true);

        _agent.baseOffset = 0.0f;
        _isExit = true;

        // another AI
        _collider.enabled = true;
        _agent.isStopped = false;


        _waitTime = _tycoonManager.CustomerWaitTime;

        _foodCreater.UnsubscribeCreateFoodEvent(this);

        _co = null;
    }
    #endregion
}
