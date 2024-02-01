using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    public int Index;
    public FoodSO FoodData;
    [SerializeField] private Image _foodImage;
    private Button _foodButton;
    private RestaurantUI _restaurantUI;

    [Header("음식 설명창")]
    [SerializeField] private GameObject _foodInfoPanel;
    [SerializeField] private TMP_Text _foodNameText;
    [SerializeField] private TMP_Text _foodDescriptionText;
    [SerializeField] private TMP_Text _foodPriceText;
    [SerializeField] private Image _infofoodImage;


    [Header("재료 개수")]
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image[] _ingredientImage = new Image[6];
    [SerializeField] private TMP_Text[] _ingredienName = new TMP_Text[6];
    [SerializeField] private TMP_Text[] _needCountText = new TMP_Text[6];
    [SerializeField] private TMP_Text[] _myGroceryCount = new TMP_Text[6];
    [SerializeField] private Button _decreaseButton;
    [SerializeField] private Button _increaseButton;
    [SerializeField] private Button _addfoodButton;
    [SerializeField] private int[] _InvenGroceryCount = new int[6];
    

    private List<ItemSlot> _InvenData;
    private int _maxCustomer;
    private int _makeFoodCount;
    private int[] _groceryCount = new int[6];
    private int _grocerykindCount;
    private void Start()
    {
        _restaurantUI= GameManager.instance.UIManager._popupDic[UIName.RestaurantUI].GetComponent<RestaurantUI>();
        _maxCustomer = TycoonManager.instance.TodayMaxCustomerNum;
        _makeFoodCount = 1;
        _grocerykindCount = FoodData.Ingredients.Count;

        for (int i = 0; i < 6; i++)
        {
            if (i < _grocerykindCount)
            {
                _groceryCount[i] = FoodData.Ingredients[i].FoodNum;
                continue;
            }
            _groceryCount[i]= 0;
        }
        
        _foodButton = GetComponent<Button>();
        _foodButton.onClick.AddListener(ClickFood);

        _foodImage = transform.GetChild(0).GetComponent<Image>();
        _foodImage.sprite = FoodData.FoodSprite;

        for (int i = 0; i < 6; i++)
        {
            if (i < _grocerykindCount)
            {
                _needCountText[i].text = "/  "+(_makeFoodCount * _groceryCount[i]).ToString();
                continue;
            }
            _needCountText[i].text = null;
        }
    }
    private void ClickFood() 
    {
        _InvenData = GameManager.instance.Inventory.Slots;
        for (int i = 0; i < FoodData.Ingredients.Count; i++)
        {
            for (int j = 0; j < _InvenData.Count; j++)
            {
                if (FoodData.Ingredients[i].IngredientSO == _InvenData[j].Item)
                {
                    _InvenGroceryCount[i] = _InvenData[j].Quantity;
                    break;   
                }
                _InvenGroceryCount[i] = 0;
            }

        }

        for (int i = 0; i < 6; i++)
        {
            if (i< _grocerykindCount)
            {
                _myGroceryCount[i].text = _InvenGroceryCount[i].ToString();//인벤개수
                _ingredientImage[i].color = new Color(255, 255, 255, 1f);
                _ingredientImage[i].sprite = FoodData.Ingredients[i].IngredientSO.Sprite;
                _ingredienName[i].text = FoodData.Ingredients[i].IngredientSO.ObjName;
                continue;
            }
            _ingredientImage[i].sprite = null;
            _ingredientImage[i].color = new Color(255, 255, 255, 0f);
            _ingredienName[i].text= null;
            _myGroceryCount[i].text = null;//인벤개수
        }
        

        _makeFoodCount = 1;
        _foodInfoPanel.SetActive(true);

        _addfoodButton.onClick.RemoveAllListeners();
        _increaseButton.onClick.RemoveAllListeners();
        _decreaseButton.onClick.RemoveAllListeners();
        
        _addfoodButton.onClick.AddListener(AddMenu);
        _increaseButton.onClick.AddListener(IncreaseCount);
        _decreaseButton.onClick.AddListener(DecreaseCount);
        GroceryText();

        _foodNameText.text = FoodData.FoodName;
        _foodDescriptionText.text = FoodData.Description;
        _foodPriceText.text = FoodData.Price.ToString();
        _infofoodImage.sprite= FoodData.FoodSprite;
    }

    private void AddMenu()
    {
        GameManager.instance.DataManager.AddMenu(FoodData, _makeFoodCount);
        gameObject.GetComponent<Button>().interactable = false;
        for (int i = 0; i < _grocerykindCount; i++)
        {
            GameManager.instance.Inventory.RemoveItem(FoodData.Ingredients[i].IngredientSO, _groceryCount[i] * _makeFoodCount);
            _InvenGroceryCount[i] = 0;
        }

        _restaurantUI.AddMenuButton();


    }

    private void DecreaseCount()
    {
        if (_makeFoodCount > 1)
        {
            _makeFoodCount--;
        }
        
        GroceryText();
    }
    private void IncreaseCount()
    {
        if (_maxCustomer - _restaurantUI.AddMenus - _makeFoodCount > 0)
        {
            _makeFoodCount++;
        }
        GroceryText();
    }

    private void GroceryText() 
    {
        _countText.text = _makeFoodCount.ToString();
        

        for (int i = 0; i < 6; i++)
        {
            if (i < _grocerykindCount)
            {
                _needCountText[i].text = "/  "+(_makeFoodCount * _groceryCount[i]).ToString();
                continue;
            }
            _needCountText[i].text = null;
        }

        for (int i = 0; i < 6; i++)
        {
            if (_makeFoodCount * _groceryCount[i] <= _InvenGroceryCount[i]&& _restaurantUI.AddMenus != _maxCustomer)
            {
                _addfoodButton.interactable = true;
            }
            else
            {
                _addfoodButton.interactable = false;
                break;
            }
        }
    }
}
