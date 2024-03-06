public class CustomerAngryState : CustomerBaseState
{
    public CustomerAngryState(CustomerStateMachine customerStateMachine) : base(customerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        _customer.TargetFood = null;
        _customer.WaitTime = _tycoonManager.CustomerWaitTime;

        _customer.OrderFoodCanvas.InactiveUI();

        _customer.CustomerAnimator.SetTrigger(AnimationParameterName.TycoonAngry);

        ++_tycoonManager.AngryCustomerNum;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
