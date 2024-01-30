using System.Collections.Generic;
using UnityEngine;

public class OrderFood 
{
    public FoodSO foodSO;
    public int foodCount;
}

public class DataManager : MonoBehaviour
{
    public FoodSO[] FoodSoDatas = new FoodSO[20];
    public List<OrderFood> Orders = new List<OrderFood>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < FoodSoDatas.Length; i++)
            {
                for (int j = 0; j < FoodSoDatas[i].Ingredients.Count; j++)
                {
                    GameManager.instance.Inventory.AddItem(FoodSoDatas[i].Ingredients[j].IngredientSO);
                }
            }
        }
    }

    public void AddMenu(FoodSO foodData, int count)
    {
        OrderFood orderFood = new OrderFood();
        orderFood.foodSO = foodData;
        orderFood.foodCount = count;
        Orders.Add(orderFood);
    }

    public void RemoveOrderData()
    {
        Orders.RemoveRange(0, Orders.Count);
    }

    public void DecideBreadNum()
    {
        int currentOrderFoodsNum = 0;
        for(int i = 0; i< Orders.Count; ++i)
        {
            currentOrderFoodsNum += Orders[i].foodCount;
        }
        int breadNum = TycoonManager.Instance.TodayMaxCustomerNum - currentOrderFoodsNum;

        AddMenu(FoodSoDatas[0], breadNum);
    }
}
