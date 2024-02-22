using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    #region Field
    [SerializeField] public FoodSO _FoodSO;    
    [SerializeField] private string _foodName;
    [SerializeField] private List<GameObject> _edibleFoods;

    private CapsuleCollider _collider;

    private bool _canHold;
    private bool _shouldClean = false;
    private FoodPlace _currentFoodPlace;
    #endregion

    #region Property
    public bool CanHold {
        get { return _canHold; }
        set { _canHold = value; } 
    }

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

    #endregion

    private void Awake()
    {
        gameObject.tag = TagName.CookedFood;
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
        yield return TycoonManager.Instance._waitForCustomerEatTime;
        _collider.enabled = true;
    }
}
