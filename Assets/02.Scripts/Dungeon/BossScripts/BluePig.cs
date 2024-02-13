using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BluePigAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer[] meshRenderers;
    readonly private float _waitTime = 5;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(0.2f, 0.9f, 0.9f));
            meshRenderers[x].SetPropertyBlock(propBlock);
        }

    }

    protected override Node SetupBehaviorTree()
    {
        Node root = new SelectorNode(new List<Node>
        {
            new DashToPlayer(_playerTransform, _pigTransform, _navMeshAgent,_waitTime),
        });
        return root;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(20);
             collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5 + Vector3.back*5, ForceMode.Impulse);

        }
    }

}