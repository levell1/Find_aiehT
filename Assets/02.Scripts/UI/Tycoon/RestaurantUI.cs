using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantUI : BaseUI
{
    [SerializeField] private GameObject _recipePanel;
    [SerializeField] private GameObject _foodInfoPanel;
    [SerializeField] private Button _addfoodButton;
    [SerializeField] private Button _addfoodButtonCheck;
    [SerializeField] private GameObject _addfoodButtonPanel;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private FoodSO[] _foodDatas = new FoodSO[20];
    [SerializeField] private RecipeSlot[] _recipeSlots;

    [Header("기본음식")]
    [SerializeField] private TMP_Text _basicFoodName;
    [SerializeField] private Image _basicFoodImage;
    [SerializeField] private TMP_Text _basicFoodPrice;

    [Header("좌측메뉴")]
    [SerializeField] private Button[] _addMenuButton = new Button[4];
    [SerializeField] private GameObject[] _addedMenu = new GameObject[4];
    [SerializeField] private Image[] _addMenuImage = new Image[4];
    [SerializeField] private TMP_Text[] _addMenuName = new TMP_Text[4];
    [SerializeField] private TMP_Text[] _addMenuCount = new TMP_Text[4];
    [SerializeField] private TMP_Text[] _addMenuPrice = new TMP_Text[4];
    [SerializeField] private TMP_Text _menuCount;

    public int AddMenus = 0;

    private void OnEnable()
    {
        _recipePanel.SetActive(false);
        GameManager.instance.DataManager.RemoveOrderData();
        for (int i = 0; i < _addMenuButton.Length; i++)
        {
            _addMenuButton[i].gameObject.SetActive(true);
            _addedMenu[i].SetActive(false);
        }
        for (int i = 0; i < _recipeSlots.Length; i++)
        {
            _recipeSlots[i].GetComponent<Button>().interactable = true;
        }
    }
    private void Start()
    {
        _menuCount.text = AddMenus.ToString() + " / " + TycoonManager.Instance.TodayMaxCustomerNum.ToString();
        _foodDatas = GameManager.instance.DataManager.FoodSoDatas;
        _basicFoodName.text = _foodDatas[0].FoodName;
        _basicFoodImage.sprite = _foodDatas[0].FoodSprite;
        _basicFoodPrice.text = _foodDatas[0].Price.ToString();
        for (int i = 0; i < _recipeSlots.Length; i++)
        {
            _recipeSlots[i].GetComponent<Button>().interactable = true;
            _recipeSlots[i].Index = i;
            _recipeSlots[i].FoodData = _foodDatas[i + 1];
        }


        for (int i = 0; i < _addMenuButton.Length; i++)
        {
            _addMenuButton[i].onClick.AddListener(ShowRecipe);
        }

        _addfoodButtonCheck.onClick.AddListener(AddMenuButton);
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void ShowRecipe()
    {
        _recipePanel.SetActive(true);
        _foodInfoPanel.SetActive(false);
    }

    public void AddMenuButton()
    {
        for (int i = 0; i < _addedMenu.Length; i++)
        {
            if (!_addedMenu[i].activeSelf)
            {
                _addMenuButton[i].gameObject.SetActive(false);
                _addedMenu[i].SetActive(true);
                _recipePanel.SetActive(false);
                _addMenuImage[i].sprite = GameManager.instance.DataManager.Orders[i].foodSO.FoodSprite;
                _addMenuName[i].text = GameManager.instance.DataManager.Orders[i].foodSO.FoodName;
                _addMenuCount[i].text = GameManager.instance.DataManager.Orders[i].foodCount.ToString();
                _addMenuPrice[i].text = GameManager.instance.DataManager.Orders[i].foodSO.Price.ToString();
                AddMenus += GameManager.instance.DataManager.Orders[i].foodCount;
                break;
            }
        }
        _menuCount.text = AddMenus.ToString() + " / " + TycoonManager.Instance.TodayMaxCustomerNum.ToString();
        _addfoodButtonPanel.SetActive(false);
        // 추가된 메뉴 , 개수 정보 전달
    }

    private void StartGame()
    {
        GameManager.instance.UIManager.CloseAllCanvas();

        TycoonManager.Instance.DecideTodayFoods();
        TycoonManager.Instance.TycoonGameStart();
    }

}
