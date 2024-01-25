using System.Collections;
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

    [Header("음식 설명창")]
    [SerializeField] private TMP_Text _foodNameText;
    [SerializeField] private TMP_Text _foodDescriptionText;
    [SerializeField] private TMP_Text _foodPriceText;
    [SerializeField] private Image _infofoodImage;
    [SerializeField] private Image _ingredientImage;

    [Header("재료 개수")]
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text needCountText;
    [SerializeField] private TMP_Text myGroceryCount;
    [SerializeField] private Button _decreaseButton;
    [SerializeField] private Button _increaseButton;
    private int _makeFoodCount;
    private int _groceryCount;

    private void Start()
    {
        _makeFoodCount = 1;
        //myGroceryCount.text = 이벤에서의 개수

        _groceryCount = FoodData.Ingredients[0].FoodNum;
        _foodButton = GetComponent<Button>();
        _foodButton.onClick.AddListener(ClickFood);

        _foodImage = transform.GetChild(0).GetComponent<Image>();
        _foodImage.sprite = FoodData.FoodSprite;


        needCountText.text = (_makeFoodCount * _groceryCount).ToString();
        _increaseButton.onClick.AddListener(IncreaseCount);
        _decreaseButton.onClick.AddListener(DecreaseCount);
    }

    private void ClickFood() 
    {
        _makeFoodCount = 1;
        GroceryText();
        _foodNameText.text = FoodData.FoodName;
        _foodDescriptionText.text = FoodData.Description;
        _foodPriceText.text = FoodData.Price.ToString();
        _infofoodImage.sprite= FoodData.FoodSprite;
        _ingredientImage.sprite = FoodData.Ingredients[0].IngredientSO.Sprite;
    }
    private void DecreaseCount()
    {
        _makeFoodCount--;
        GroceryText();
    }
    private void IncreaseCount()
    {
        _makeFoodCount++;
        GroceryText();
    }

    private void GroceryText() 
    {
        countText.text = _makeFoodCount.ToString();
        needCountText.text = (_makeFoodCount * _groceryCount).ToString();
    }


}
