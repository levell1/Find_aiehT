using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SequenceNode : Node
{
    private List<Node> _children;
    private int currentChildIndex = 0;
    public SequenceNode() : base() { }

    public SequenceNode(List<Node> children) : base(children) { this._children = children; }

    public override NodeState Evaluate()
    {
/*        if (_children == null || _children.Count == 0)
            return NodeState.Failure;
        foreach (var child in _children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Failure:
                    return state = NodeState.Failure;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    return state = NodeState.Running;
            }
        }

        return state = NodeState.Success;*/

        // 현재 자식 노드 실행
        var currentChild = _children[currentChildIndex];
        var result = currentChild.Evaluate();

        if (result == NodeState.Success)
        {
            currentChildIndex++;
            if (currentChildIndex >= _children.Count)
            {
                currentChildIndex = 0;
                return NodeState.Success;
            }
        }
        else if (result == NodeState.Failure)
        {
            return NodeState.Failure;
        }
        return NodeState.Running;
    }
}
