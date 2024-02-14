using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickBulletBT : Tree
{
    private Transform _playerTransform;
    private Transform _chickTransform;
    private NavMeshAgent _navMeshAgent;
    readonly private float _runDamage = 10f;
    readonly private float _waitTime = 1f;
    private void Awake()
    {
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _chickTransform = gameObject.transform;
    }

    protected override Node SetupBehaviorTree()
    {
        Node root = new SelectorNode(new List<Node>
        {
            new DashToPlayer(_playerTransform, _chickTransform, _navMeshAgent, _waitTime),
        });
        return root;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_runDamage);
        }
    }
}
