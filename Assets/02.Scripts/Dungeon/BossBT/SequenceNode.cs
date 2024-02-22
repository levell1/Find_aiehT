using System.Collections.Generic;

public class SequenceNode : Node
{
    private List<Node> _children;
    private int currentChildIndex = 0;
    public SequenceNode() : base() { }

    public SequenceNode(List<Node> children) : base(children) { this._children = children; }

    public override NodeState Evaluate()
    {
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
