using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenRecipeSlot : MonoBehaviour
{
    public int Index;
    public FoodSO FoodData;
    [SerializeField] private Image _foodImage;
    private Button _foodButton;

    [Header("음식 설명창")]
    [SerializeField] private GameObject _foodInfoPanel;
    [SerializeField] private TMP_Text _foodNameText;
    [SerializeField] private TMP_Text _foodDescriptionText;
    [SerializeField] private TMP_Text _foodPriceText;
    [SerializeField] private Image _infoFoodImage;

    [Header("재료 개수")]
    [SerializeField] private Image[] _ingredientImage = new Image[6];
    [SerializeField] private TMP_Text[] _ingredienName = new TMP_Text[6];
    [SerializeField] private TMP_Text[] _needCountText = new TMP_Text[6];

    private int[] _ingredientCount = new int[6];
    private int _ingredientKindCount;

    private void OnEnable()
    {
        _ingredientKindCount = FoodData.Ingredients.Count;

        for (int i = 0; i < 6; ++i)
        {
            if (i < _ingredientKindCount)
            {
                _ingredientCount[i] = FoodData.Ingredients[i].FoodNum;
                continue;
            }
            _ingredientCount[i] = 0;
        }

        _foodButton = GetComponent<Button>();
        _foodButton.onClick.AddListener(ClickFood);
        _foodImage = transform.GetChild(0).GetComponent<Image>();
        _foodImage.sprite = FoodData.FoodSprite;

    }

    private void ClickFood()
    {
        _foodInfoPanel.SetActive(true);

        for (int i = 0; i < 6; ++i)
        {
            if (i < _ingredientKindCount)
            {
                _ingredientImage[i].color = new Color(255, 255, 255, 1f);
                _ingredientImage[i].sprite = FoodData.Ingredients[i].IngredientSO.Sprite;
                _ingredienName[i].text = FoodData.Ingredients[i].IngredientSO.ObjName;
                continue;
            }
            _ingredientImage[i].sprite = null;
            _ingredientImage[i].color = new Color(255, 255, 255, 0f);
            _ingredienName[i].text = null;
        }

        _foodNameText.text = FoodData.FoodName;
        _foodDescriptionText.text = FoodData.Description;
        _foodPriceText.text = FoodData.Price.ToString();
        _infoFoodImage.sprite = FoodData.FoodSprite;

        for (int i = 0; i < 6; ++i)
        {
            if (i < _ingredientKindCount)
            {
                _needCountText[i].text = _ingredientCount[i].ToString();
                continue;
            }
            _needCountText[i].text = null;
        }
    }
}
