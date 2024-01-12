using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField] private List<Transform> _destinations;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        // 순차적으로
        // 랜덤 -> 자리에 있다면 다시 랜덤?
        int randomDestinationNum = Random.Range(0, _destinations.Count);
        _agent.SetDestination(_destinations[randomDestinationNum].position);
        
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
}
