using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _targetFood;

    private FoodPlace _targetFoodPlace;
    public FoodPlace TargetFoodPlace
    {
        get {  return _targetFoodPlace; }
        set
        {
            _targetFoodPlace = value;
            _targetFoodPlace.OnCustomerGetFood += PlayGetFoodAnition;
        }
    }

    public int SeatNum { get; set; }

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
        //TODO : 순차적으로 or 랜덤 -> 자리에 있다면 다시 랜덤?
        set
        {
            _agent.SetDestination(value.position);
        }
    }

    CustomerController()
    {

    }


    private void OnEnable()
    {
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
        }
    }

    private void PlayGetFoodAnition()
    {
        _animator.SetTrigger("GetFood");
    }
}
