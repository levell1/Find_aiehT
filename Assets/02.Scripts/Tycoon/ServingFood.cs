using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingFood : MonoBehaviour
{
    [SerializeField] private Transform _handTransform;

    private GameObject _canHoldFood;
    private GameObject _holdingFood;
    private List<GameObject> _canCleaningFoods = new();

    private const float _minDistanceToPutFood = 2f;
    
    public bool IsTycoonGameOver
    {
        get { return _isTycoonGameOver; }
        set { _isTycoonGameOver = value; }
    }
    private bool _isTycoonGameOver = false;

    public GameObject CanHoldFood
    {
        get { return _canHoldFood; }
        set { _canHoldFood = value; }
    }
    public bool CanThrowAway { get; set; }
    public bool CanOpenRecipeUI { get; set; }
    public bool CanExitRestaurant { get; set; }
    public void TycoonInteraction()
    {
        if (CanOpenRecipeUI)
        {
            GameManager.Instance.UIManager.ShowCanvas(UIName.RestaurantUI);
        }

        if (CanExitRestaurant)
        {
            CanExitRestaurant = false;
            GameManager.Instance.Player.transform.position = new Vector3(4, 0, -160);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        if (_holdingFood != null)
        {
            if (CanThrowAway)
                ThrowAwayFood();
            else
                PutdownFood();
        }
        else if (_canCleaningFoods.Count > 0)
        {
            CleaningFood();
        }
        else if (_canHoldFood != null)
        {
            PickupFood();
        }
    }

    #region Event

    public event Action OnCheckCreateStation;

    #endregion

    private void PickupFood()
    {
        _canHoldFood.GetComponentInParent<FoodPlace>().CurrentFood = null;
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.PutDownFood, Vector3.zero, 0.3f);
        _holdingFood = _canHoldFood;
        _canHoldFood = null;

        _holdingFood.transform.position = _handTransform.position;
        _holdingFood.transform.SetParent(_handTransform);
        _holdingFood.GetComponent<Collider>().enabled = false;
        _holdingFood.GetComponent<CookedFood>().CurrentFoodPlace = null;

        OnCheckCreateStation?.Invoke();
    }

    private void PutdownFood()
    {
        float minDistance = _minDistanceToPutFood;
        FoodPlace foodPlace = null;

        for (int i = 0; i < TycoonManager.Instance.ServingStations.Count; ++i)
        {
            GameObject station = TycoonManager.Instance.ServingStations[i];
            FoodPlace stationFood = station.GetComponent<FoodPlace>();
            
            if (stationFood.CurrentFood == null)
            {
                float d = Vector3.Distance(_handTransform.position, station.transform.position);
                if (d < minDistance)
                {
                    minDistance = d;
                    foodPlace = stationFood;
                }
            }
        }

        if (foodPlace != null)
        {
            _holdingFood.GetComponent<Collider>().enabled = true;
            _holdingFood.transform.SetParent(foodPlace.transform);
            _holdingFood.transform.localPosition = Vector3.zero;
            
            foodPlace.CurrentFood = _holdingFood.GetComponent<CookedFood>();
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.PutDownFood, Vector3.zero, 0.3f);
            _holdingFood = null;
        }
    }

    private void CleaningFood()
    {
        int lastIndex = _canCleaningFoods.Count - 1;
        GameObject food = _canCleaningFoods[lastIndex];
        food.GetComponent<CookedFood>().CurrentFoodPlace = null;
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.PutDownFood,Vector3.zero, 0.3f);
        _canCleaningFoods.Remove(food);
        Destroy(food);
    }

    private void ThrowAwayFood()
    {
        Destroy(_holdingFood.gameObject);
    }

    public void ReturnPlayerSetting()
    {
        if (_holdingFood != null)
            ThrowAwayFood();

        _isTycoonGameOver = true;
        _canCleaningFoods.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.CookedFood))
        {
            CookedFood cookedFood = other.gameObject.GetComponent<CookedFood>();
            if (cookedFood != null)
            {
                if (cookedFood.ShouldClean && !_isTycoonGameOver)
                {
                    if (!_canCleaningFoods.Contains(other.gameObject))
                    {
                        _canCleaningFoods.Add(other.gameObject);
                    }
                }
                else if (_holdingFood == null)
                {
                    _canHoldFood = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.CookedFood))
        {
            CookedFood cookedFood = other.gameObject.GetComponent<CookedFood>();
            if (cookedFood != null)
            {
                if (cookedFood.ShouldClean)
                {
                    if (_canCleaningFoods.Contains(other.gameObject))
                    {
                        _canCleaningFoods.Remove(other.gameObject);
                    }
                }
                else if (_canHoldFood == other.gameObject)
                {
                    _canHoldFood = null;
                }
            }
        }
    }
}
