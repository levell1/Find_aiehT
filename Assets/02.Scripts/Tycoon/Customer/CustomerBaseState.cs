public class CustomerBaseState : ICustomerState
{
    protected CustomerStateMachine _stateMachine;
    protected TycoonManager _tycoonManager;
    protected Customer _customer;

    public CustomerBaseState(CustomerStateMachine customerStateMachine)
    {
        _stateMachine = customerStateMachine;
    }

    public virtual void Enter()
    {
        _tycoonManager = TycoonManager.Instance;
        _customer = _stateMachine.Customer;
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }
}
