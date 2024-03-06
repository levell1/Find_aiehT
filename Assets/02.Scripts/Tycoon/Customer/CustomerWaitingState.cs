using System.Collections.Generic;

public class CustomerWaitingState : CustomerBaseState
{
    private const float _agentBaseOffset = 0.57f;

    public CustomerWaitingState(CustomerStateMachine customerStateMachine) : base(customerStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        SelectFood();

        _customer.CustomerAgent.baseOffset = _agentBaseOffset;
        _customer.transform.rotation = _customer.TargetFoodPlace.gameObject.transform.rotation;
        _customer.CustomerCollider.enabled = false;
        _customer.CustomerAgent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void SelectFood()
    {
        List<OrderFood> menu = _tycoonManager.TodayFoods;

        int targetFoodNum = UnityEngine.Random.Range(0, menu.Count);
        _customer.TargetFood = menu[targetFoodNum].FoodSO;

        _customer.OrderFoodCanvas.SelectFood.sprite = menu[targetFoodNum].FoodSO.FoodSprite;
        _customer.OrderFoodCanvas.ActiveUI();

        _tycoonManager.CookingUI.StartCooking(menu[targetFoodNum].FoodSO);

        --menu[targetFoodNum].FoodCount;
        if (menu[targetFoodNum].FoodCount == 0)
            menu.Remove(menu[targetFoodNum]);
    }
}
