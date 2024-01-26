using System.Collections.Generic;
using UnityEngine;

public class orderFood 
{
    public FoodSO foodSO;
    public int foodCount;
}

public class DataManager : MonoBehaviour
{
    public FoodSO[] FoodSoDatas = new FoodSO[20];
    public List<orderFood> Orders = new List<orderFood>();

    public void AddMenu(FoodSO fooddata, int count) 
    {
        orderFood orderFood = new orderFood();
        orderFood.foodSO = fooddata;
        orderFood.foodCount = count;
        Orders.Add(orderFood);
    }

    public void RemoveOrderData() 
    {
        Orders.RemoveRange(0,Orders.Count);
    }
}
