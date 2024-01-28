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
}
