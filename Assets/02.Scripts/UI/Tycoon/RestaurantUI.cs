using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestaurantUI : BaseUI
{
    [SerializeField] private GameObject _recipePanel;
    [SerializeField] private GameObject _foodInfoPanel;
    [SerializeField] private Button _addFoodButton;
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
        // 타이쿤씬에서 instantiate 하는걸로 바꾸기.
        if (GameManager.Instance.DataManager.Orders.Count==0 && SceneManager.GetActiveScene().name==SceneName.TycoonScene)
        {
            _recipePanel.SetActive(false);
            AddMenus = 0;
            _menuCount.text = AddMenus.ToString() + " / " + TycoonManager.Instance.TodayMaxCustomerNum.ToString();
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
        
    }
    private void Start()
    {
        
        _foodDatas = GameManager.Instance.DataManager.FoodSODatas;
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

        _startGameButton.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.UIManager.CloseLastCanvas();
        }
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
                _addMenuImage[i].sprite = GameManager.Instance.DataManager.Orders[i].FoodSO.FoodSprite;
                _addMenuName[i].text = GameManager.Instance.DataManager.Orders[i].FoodSO.FoodName;
                _addMenuCount[i].text = GameManager.Instance.DataManager.Orders[i].FoodCount.ToString();
                _addMenuPrice[i].text = GameManager.Instance.DataManager.Orders[i].FoodSO.Price.ToString();
                AddMenus += GameManager.Instance.DataManager.Orders[i].FoodCount;
                break;
            }
        }
        _menuCount.text = AddMenus.ToString() + " / " + TycoonManager.Instance.TodayMaxCustomerNum.ToString();
        // 추가된 메뉴 , 개수 정보 전달
    }

    private void StartGame()
    {
        GameManager.Instance.DataManager.RemoveOrderData();
        GameManager.Instance.UIManager.CloseAllCanvas();
        TycoonManager.Instance.TycoonGameStart();
    }

}
