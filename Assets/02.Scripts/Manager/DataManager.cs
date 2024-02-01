using System.Collections.Generic;
using UnityEngine;

public class OrderFood 
{
    public FoodSO FoodSO;
    public int FoodCount;
}

public class DataManager : MonoBehaviour
{
    public FoodSO[] FoodSODatas = new FoodSO[20];
    public List<OrderFood> Orders = new List<OrderFood>();

    public void AddMenu(FoodSO foodData, int count)
    {
        OrderFood orderFood = new OrderFood();
        orderFood.FoodSO = foodData;
        orderFood.FoodCount = count;
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
            currentOrderFoodsNum += Orders[i].FoodCount;
        }
        int breadNum = TycoonManager.Instance.TodayMaxCustomerNum - currentOrderFoodsNum;

        AddMenu(FoodSODatas[0], breadNum);
    }
}
