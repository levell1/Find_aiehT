using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    #region Field
    private TycoonManager _tycoonManager;

    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _targetFood;
    private bool isGetFood = false;

    private FoodPlace _targetFoodPlace;
    public FoodPlace TargetFoodPlace
    {
        get {  return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            // TODO: event 해제?
            _targetFoodPlace.OnCustomerGetFood += GetFood;
        }
    }

    public GameObject TargetFood
    {
        get { return _targetFood; }
        set
        {
            _targetFood = value;
            Debug.Log(_targetFood.name);
        }
    }

    public Transform AgentDestination
    {
        set
        {
            _agent.SetDestination(value.position);
        }
    }
    
    public int AgentPriority
    {
        set
        {
            _agent.avoidancePriority = value;
        }
    }

    #endregion

    #region Event

    //public event Action OnCustomerExit;

    #endregion

    private void OnEnable()
    {
        _tycoonManager = GameManager.instance.TycoonManager;

        _agent = GetComponent<NavMeshAgent>();

        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("IsWalk", true);

        //SelectFood();
    }

    private void Update()
    {
        if(!_agent.hasPath)
        {
            _animator.SetBool("IsWalk", false);
            transform.rotation = Quaternion.identity;

            if (isGetFood)
            {
                isGetFood = false;

                //OnCustomerExit?.Invoke();

                GameManager.instance.PoolingManager.ReturnObject(gameObject);
            }
        }
    }

    //private void SelectFood()
    //{
    //    int targetFoodNum = UnityEngine.Random.Range(0, _tycoonManager.CustomerTargetFoodPrefabs.Count);
    //    _targetFood = _tycoonManager.CustomerTargetFoodPrefabs[targetFoodNum];
    //}

    private void GetFood()
    {
        _animator.SetTrigger("GetFood");
        StartCoroutine(ExitRestaurant());
    }


    #region Coroutine

    IEnumerator ExitRestaurant()
    {
        yield return new WaitForSeconds(3f);

        _agent.SetDestination(GameManager.instance.TycoonManager.CreateCustomerPos.position);
        _animator.SetBool("IsWalk", true);
        isGetFood = true;
    }

    #endregion
}
