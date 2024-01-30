using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    [SerializeField] public FoodSO _FoodSO;     // FoodPlace에서 사용
    [SerializeField] private string _foodName;
    [SerializeField] private List<GameObject> _edibleFoods;

    private CapsuleCollider _collider;

    public string FoodName
    {
        get { return _foodName; }
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
        set
        { 
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
            if (value == null)
            {
                _currentFoodPlace.OnCustomerGetFood -= SetColliderEnable;
                _currentFoodPlace.CurrentFood = null;
                StopAllCoroutines();
            }
            else if(value.CurrentCustomer != null)
            {
                value.OnCustomerGetFood += SetColliderEnable;
            }
            _currentFoodPlace = value;
        }
    }

    private void Awake()
    {
        gameObject.tag = "CookedFood";
        _collider = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        _canHold = true;
        _shouldClean = false;

        foreach (GameObject food in _edibleFoods)
            food.SetActive(true);
    }

    private void SetColliderEnable()
    {
        StartCoroutine(ColliderControl());
    }

    IEnumerator ColliderControl()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(10f);
        _collider.enabled = true;
    }
}
