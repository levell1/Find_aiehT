using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BluePigAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer[] _meshRenderers;
    readonly private float _waitTime = 3;
    readonly private float _knockBack = 5f;
    readonly private float _knockBackCount = 5;
    readonly private float _Dagage = 500f;
    private Vector3 _power;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(0.2f, 0.9f, 0.9f));
            _meshRenderers[i].SetPropertyBlock(propBlock);
        }

    }

    protected override Node SetupBehaviorTree()
    {
        Node root = new SelectorNode(new List<Node>
        {
            new DashToPlayer(_playerTransform, _pigTransform, _navMeshAgent,_waitTime),
            new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
        });
        return root;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_Dagage);
            StartCoroutine(KnockBack(5f));
        }
    }

    IEnumerator KnockBack(float knockBack)
    {
        Vector3 direction = _playerTransform.position - _pigTransform.position;
        direction.y = 0;
        _power = Vector3.zero;
        int count = 0;
        while (count < _knockBackCount)
        {
            _power += direction.normalized * _knockBack;
            _playerTransform.gameObject.GetComponent<Rigidbody>().AddForce(_power, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.01f);
            count++;
        }
    }

}