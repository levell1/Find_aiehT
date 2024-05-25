using UnityEngine;

public class CustomerExitingState : CustomerBaseState
{
    public CustomerExitingState(CustomerStateMachine customerStateMachine) : base(customerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        _customer.CustomerAgent.SetDestination(_tycoonManager.CustomerCreatePos.position);
        _customer.CustomerAnimator.SetBool(AnimationParameterName.TycoonIsWalk, true);
        _customer.CustomerAgent.baseOffset = 0.0f;

        // 다른 AI와 충돌을 위한 처리
        _customer.CustomerCollider.enabled = true;
        _customer.CustomerAgent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();

        GameManager.Instance.PoolingManager.ReturnObject(_customer.gameObject);
    }
}
