using UnityEngine;

public class CustomerEnterState : CustomerBaseState
{
    public CustomerEnterState(CustomerStateMachine customerStateMachine) : base(customerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        int seatIndex = _tycoonManager.DecideCustomerDestination(_customer);
        _tycoonManager.SetCustomerFoodPlace(_customer, seatIndex);

        _customer.WaitTime = _tycoonManager.CustomerWaitTime;
        _customer.AgentPriority = ++_tycoonManager.AgentPriority;
        _customer.CustomerAnimator.SetBool(AnimationParameterName.TycoonIsWalk, true);

        _customer.transform.rotation = Quaternion.identity;
        _customer.CustomerAnimator.gameObject.transform.localPosition = Vector3.zero;
        _customer.CustomerAnimator.gameObject.transform.localRotation = Quaternion.identity;
    }

    public override void Exit()
    {
        base.Exit();

        _customer.CustomerAnimator.SetBool(AnimationParameterName.TycoonIsWalk, false);
    }
}
