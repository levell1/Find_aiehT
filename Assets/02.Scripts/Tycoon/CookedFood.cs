using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    [SerializeField] private string _foodName;
    public string FoodName
    {
        get { return _foodName; }
        private set { }
    }
}
