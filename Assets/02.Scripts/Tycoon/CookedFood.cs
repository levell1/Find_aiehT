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

    private bool _canHold;
    public bool CanHold {
        get { return _canHold; }
        set { _canHold = value; } 
    }

    private void Awake()
    {
        gameObject.tag = "CookedFood";
    }

    private void OnEnable()
    {
        _canHold = true;
    }
}
