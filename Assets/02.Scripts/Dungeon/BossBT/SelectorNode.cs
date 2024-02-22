using System.Collections.Generic;


public class SelectorNode : Node
{
    private List<Node> children;
    private int currentChildIndex = 0;
    public SelectorNode() : base() { }

    public SelectorNode(List<Node> children) : base(children) { this.children = children; }

    public override NodeState Evaluate()
    {

        var currentChild = children[currentChildIndex];
        var result = currentChild.Evaluate();


        if (result == NodeState.Success || result == NodeState.Failure)
        {
            currentChildIndex++;
            if (currentChildIndex >= children.Count)
            {
                currentChildIndex = 0;
                return NodeState.Success;
            }
        }
        return NodeState.Running;
    }
}
