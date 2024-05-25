using UnityEngine;

public class CustomerEatingState : CustomerBaseState
{
    public CustomerEatingState(CustomerStateMachine customerStateMachine) : base(customerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        _customer.TargetFood = null;
        _customer.WaitTime = _tycoonManager.CustomerWaitTime;
        _customer.OrderFoodCanvas.InactiveUI();

        _customer.CustomerAnimator.SetTrigger(AnimationParameterName.TycoonGetFood);
        _customer.CustomerAnimator.SetBool(AnimationParameterName.TycoonIsEat, true);

        int random = Random.Range(1, 3);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Money, Vector3.zero, 0.5f);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Eat + random.ToString(), Vector3.zero, 1f);

        _customer.GetComponentInChildren<CustomerEffect>().PlayGetCoinEffect();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
