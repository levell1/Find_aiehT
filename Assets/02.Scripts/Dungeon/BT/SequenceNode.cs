using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SequenceNode : Node
{
    public SequenceNode() : base() { }

    public SequenceNode(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool bNowRunning = false;
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    return state = NodeState.Failure;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    bNowRunning = true;
                    continue;
                default:
                    continue;
            }
        }

        return state = bNowRunning ? NodeState.Running : NodeState.Success;

        /*// 현재 자식 노드 실행
        var currentChild = children[currentChildIndex];
        var result = currentChild.Evaluate();

        // 현재 자식 노드가 완료되었을 경우
        if (result == NodeState.Success)
        {
            // 다음 자식 노드로 이동
            currentChildIndex++;
            // 모든 자식 노드를 실행했을 경우 복합 노드 완료
            if (currentChildIndex >= children.Length)
            {
                return NodeState.Success;
            }
        }else if (result == NodeState.Failure) 
        {
            return NodeState.Failure;
        }
        // 실행 중인 경우 실행 중 상태 반환
        return NodeState.Running;*/
    }
}
