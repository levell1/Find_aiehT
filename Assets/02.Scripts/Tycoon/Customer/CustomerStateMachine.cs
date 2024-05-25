public class CustomerStateMachine
{
    protected ICustomerState currentState;

    public Customer Customer;

    public CustomerEnterState EnterState { get; }
    public CustomerWaitingState WaitingState { get; }
    public CustomerEatingState EatingState { get; }
    public CustomerExitingState ExitingState { get; }
    public CustomerAngryState AngryState { get; }

    public CustomerStateMachine(Customer customer)
    {
        Customer = customer;

        EnterState = new CustomerEnterState(this);
        WaitingState = new CustomerWaitingState(this);
        EatingState = new CustomerEatingState(this);
        ExitingState = new CustomerExitingState(this);
        AngryState = new CustomerAngryState(this);
    }

    public void ChangeState(ICustomerState state)
    {
        currentState?.Exit();

        currentState = state;

        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
