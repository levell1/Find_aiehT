using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private Node rootNode;

    protected void Start()
    {
        rootNode = SetupBehaviorTree();
    }

    protected void Update()
    {
        if (rootNode is null) return;
        rootNode.Evaluate();
    }

    protected abstract Node SetupBehaviorTree();
}
