using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    private List<Node> children;
    private int currentChildIndex = 0;
    public SelectorNode() : base() { }

    public SelectorNode(List<Node> children) : base(children) { this.children = children; }

    public override NodeState Evaluate()
    {
 /*       if (children == null)
            return NodeState.Failure;
        foreach (var child in childrenNode)
        {
            switch (child.Evaluate())
            {
                case NodeState.Success:
                    return state = NodeState.Success;
                case NodeState.Running:
                    return state = NodeState.Running;
            }
        }
        return state = NodeState.Failure;*/

        // 현재 자식 노드 실행
        var currentChild = children[currentChildIndex];
        var result = currentChild.Evaluate();

        // 현재 자식 노드가 완료되었을 경우
        if (result == NodeState.Success || result == NodeState.Failure)
        {
            // 다음 자식 노드로 이동
            currentChildIndex++;
            // 모든 자식 노드를 실행했을 경우 복합 노드 완료
            if (currentChildIndex >= children.Count)
            {
                currentChildIndex = 0;
                return NodeState.Success;
            }
        }
        // 실행 중인 경우 실행 중 상태 반환
        return NodeState.Running;
    }
}
