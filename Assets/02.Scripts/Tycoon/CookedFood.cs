using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedFood : MonoBehaviour
{
    #region Field
    [SerializeField] public FoodSO _FoodSO = null;
    [SerializeField] private string _foodName;
    [SerializeField] private List<GameObject> _edibleFoods;

    private CapsuleCollider _collider;

    private bool _shouldClean = false;
    private FoodPlace _currentFoodPlace;
    private ServingFood _playerServingFood;
    #endregion

    #region Property

    public bool ShouldClean
    {
        get { return _shouldClean; }
        set
        { 
            _shouldClean = value;

            if (_shouldClean)
            {
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
            else
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
        _shouldClean = false;

        foreach (GameObject food in _edibleFoods)
            food.SetActive(true);

        _playerServingFood = GameManager.Instance.Player.GetComponent<ServingFood>();
    }

    public void SetColliderEnable()
    {
        _collider.enabled = false;

        if(_playerServingFood.CanHoldFood == gameObject)
        {
            _playerServingFood.CanHoldFood = null;
        }
        StartCoroutine(ColliderControl());
    }

    IEnumerator ColliderControl()
    {
        yield return TycoonManager.Instance._waitForCustomerEatTime;
        _collider.enabled = true;
    }
}
