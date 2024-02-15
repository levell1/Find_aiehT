
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenPigAI : Tree
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private NavMeshAgent _navMeshAgent;
    private LevitateObject _levitateObject;
    private SkinnedMeshRenderer[] _meshRenderers;

    private void Awake()
    {
        
        _playerTransform = GameManager.Instance.Player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pigTransform = gameObject.transform;
        _levitateObject = GetComponentInChildren<LevitateObject>();
        _levitateObject.gameObject.SetActive(false);

        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        for (int x = 0; x < _meshRenderers.Length; x++)
        {
            _meshRenderers[x].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(0.6f, 1f, 0.6f));
            _meshRenderers[x].SetPropertyBlock(propBlock);
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