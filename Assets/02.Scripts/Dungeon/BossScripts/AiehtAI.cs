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
    [SerializeField] private GameObject _lightObject;

    [Header("대쉬, 충돌")]
    readonly private float _DashWaitTime = 3f;
    readonly private float _DashDamage = 1000f;
    readonly private float _knockBack = 7f;
    readonly private float _knockBackCount = 5f;

    [Header("스킬 쿨타임")]
    readonly private float _levitateTiem = 4f;
    readonly private float _LightAttackWaitTime = 2f;
  
    private Vector3 _power;


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
                   new LightAttack(_playerTransform, _pigTransform,_LightAttackWaitTime,_lightObject),
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
            health.TakeDamage(_DashDamage);
            StartCoroutine(KnockBack(5f));
        }
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.white; 
        Gizmos.DrawSphere(transform.position, 10f);
    }
   

    IEnumerator KnockBack(float knockBack)
    {
        Vector3 Direction = _playerTransform.position - _pigTransform.position;
        _power = Vector3.zero;
        Direction.y = 0;
        int count = 0;
        while (count < _knockBackCount)
        {
            _power += Direction.normalized * _knockBack;
            _playerTransform.gameObject.GetComponent<Rigidbody>().AddForce(_power, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.01f);
            count++;
        }
    }


}
