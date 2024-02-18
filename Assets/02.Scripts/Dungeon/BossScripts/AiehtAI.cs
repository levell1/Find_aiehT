using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AiehtAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private LevitateObject _levitateObject;
    readonly private float _knockBack = 7f;
    readonly private float _knockBackCount = 5f;
    readonly private float _levitateTiem = 4f;
    readonly private float _DashWaitTime = 3f;
    readonly private float _LightAttackWaitTime = 2f;
    readonly private float _runMoveSpeed = 10f;
    float time = 0;
    Vector3 Power;


    private void Awake()
    {
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        gameObject.GetComponent<BoxCollider>().enabled = false;
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
                   new CheckPlayerDistanceNotNode(_pigTransform,12.0f),
                   new LightAttack(_playerTransform, _pigTransform,_LightAttackWaitTime),
                   new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                   new CheckPlayerDistanceNode(_pigTransform,12.0f),
                   new LevitateNode(_pigTransform,_playerTransform,_levitateObject,_levitateTiem),
                   new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                   new CheckPlayerDistanceNotNode(_pigTransform,20.0f),
                   new DashToPlayer(_playerTransform, _pigTransform, _navMeshAgent,_DashWaitTime),
                   new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                }
            ),
           

        });
        return root;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(20);
            StartCoroutine(KnockBack(5f));
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.white; 
        Gizmos.DrawSphere(transform.position, 20f);
    }
   

    IEnumerator KnockBack(float knockBack)
    {
        Vector3 Direction = _playerTransform.position - _pigTransform.position;
        Power = Vector3.zero;
        int count = 0;
        while (count < _knockBackCount)
        {

            Power += Direction.normalized * _knockBack;
            _playerTransform.gameObject.GetComponent<Rigidbody>().AddForce(Power, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.01f);
            count++;
        }
    }


}
