
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenPigAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private LevitateObject _levitateObject;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        _levitateObject = GetComponentInChildren<LevitateObject>();

        _levitateObject.gameObject.SetActive(false);
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        for (int x = 0; x < meshRenderers.Length; x++)
        {
            meshRenderers[x].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(0.6f, 1f, 0.6f));
            meshRenderers[x].SetPropertyBlock(propBlock);
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
                    new CheckPlayerDistanceNode(_pigTransform,5.0f),
                    new LevitateNode(_pigTransform,_levitateObject),
                }
            ),
            new SequenceNode
            (
                new List<Node>()
                {
                    new CheckPlayerDistanceNode(_pigTransform,1.0f),
                    new RunAwayNode(_pigTransform,_navMeshAgent),
                }
            ),
           
            new RangeAttackNode(_playerTransform, _pigTransform),
        });
        return root;
    }

}