using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedPig : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer[] _meshRenderers;
    readonly private float _runDamage= 800f;
    readonly private float _waitTime = 4f;
    readonly private float _knockBack = 4f;
    readonly private float _knockBackCount = 5;
    private Vector3 _power;
    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent =GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
  
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1.0f, 0.6f, 0.6f));
            _meshRenderers[i].SetPropertyBlock(propBlock);
        }

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
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,5.0f),
                    new RunAwayNode(_pigTransform,_navMeshAgent,_navMeshAgent.speed),
                }
            ),
            new GoToPlayerNode(_playerTransform, _pigTransform, _navMeshAgent,_waitTime), 
        });
        return root;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_runDamage);
            StartCoroutine(KnockBack(5f));
        }
    }
    //블루피그 같은코드 리펙토링
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