using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    [NonSerialized] public int _seatNum = 0;

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

    public void DecideDestination(Transform destinationTransform)
    {
        //TODO : 순차적으로 or 랜덤 -> 자리에 있다면 다시 랜덤?
        _agent.SetDestination(destinationTransform.position);
    }
}
