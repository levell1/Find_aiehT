using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public FoodSO[] FoodSoDatas = new FoodSO[20];
    public Dictionary<FoodSO, int> Orders = new ();

    public void AddMenu(FoodSO fooddata, int count) 
    {
        Orders.Add(fooddata, count);
    }

    public void RemoveOrderData() 
    {
        Orders.Clear();
    }
}
