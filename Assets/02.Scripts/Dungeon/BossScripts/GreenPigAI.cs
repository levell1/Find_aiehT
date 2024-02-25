using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenPigAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private LevitateObject _levitateObject;
    readonly float _levitateTiem = 2f;

    private void Awake()
    {
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        _levitateObject = GetComponentInChildren<LevitateObject>();
        _levitateObject.gameObject.SetActive(false);
    }

    protected override Node SetupBehaviorTree()
    {
        Node root = new SelectorNode(new List<Node>
        {
             new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,2.0f),
                    new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,5.0f),
                    new LevitateNode(_pigTransform,_playerTransform,_levitateObject,_levitateTiem),
                    new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                    new RangeAttackNode(_playerTransform, _pigTransform),
                }
            ),

            new RangeAttackNode(_playerTransform, _pigTransform),
        });
        return root;
    }
}