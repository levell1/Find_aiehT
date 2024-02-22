using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private Node _rootNode;

    protected void Start()
    {
        _rootNode = SetupBehaviorTree();
    }

    protected void Update()
    {
        if (_rootNode is null) return;
        _rootNode.Evaluate();
    }

    protected abstract Node SetupBehaviorTree();
}
