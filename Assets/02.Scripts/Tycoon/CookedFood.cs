using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    [SerializeField] private FoodSO _foodSO;
    [SerializeField] private string _foodName;
    [SerializeField] private List<GameObject> _edibleFoods;
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

    private bool _shouldClean = false;
    public bool ShouldClean
    {
        get { return _shouldClean; }
        set { 
            _shouldClean = value;

            if (_shouldClean)
            {
                _canHold = false;
                foreach (GameObject food in _edibleFoods)
                    food.SetActive(false);
            }
        }
    }

    private FoodPlace _currentFoodPlace;
    public FoodPlace CurrentFoodPlace
    {
        get { return _currentFoodPlace; }
        set
        {
            if(value == null)
            {
                _currentFoodPlace.CurrentFood = null;
            }
            _currentFoodPlace = value;
        }
    }

    private void Awake()
    {
        gameObject.tag = "CookedFood";
    }

    private void OnEnable()
    {
        _canHold = true;
        _shouldClean = false;

        foreach (GameObject food in _edibleFoods)
            food.SetActive(true);
    }
}
