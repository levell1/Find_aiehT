using UnityEngine;
using UnityEngine.UI;

public class RestaurantUI : BaseUI
{
    [SerializeField] private GameObject recipePanel;
    [SerializeField] private Button[] _addMenuButton=new Button[4];
    [SerializeField] private GameObject[] _addedMenu = new GameObject[4];
    [SerializeField] private Button _addfoodButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private FoodDatas _foodDatas;
    [SerializeField] private RecipeSlot[] _recipeSlots;

    private void OnEnable()
    {
        recipePanel.SetActive(false);
    }
    private void Start()
    {
        for (int i = 0; i < _recipeSlots.Length; i++)
        {
            _recipeSlots[i].Index = i;
            _recipeSlots[i].FoodData = _foodDatas.FoodSoDatas[i];
        }

        
        for (int i = 0; i < _addMenuButton.Length; i++)
        {
            _addMenuButton[i].onClick.AddListener(ShowRecipe);
        }
        
        _addfoodButton.onClick.AddListener(AddMenuButton);
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void ShowRecipe()
    {
        recipePanel.SetActive(true);
    }

    private void AddMenuButton() 
    {
        for (int i = 0; i < _addedMenu.Length; i++)
        {
            if (!_addedMenu[i].activeSelf)
            {
                _addMenuButton[i].gameObject.SetActive(false);
                _addedMenu[i].SetActive(true);
                recipePanel.SetActive(false);
                break;
            }
        }
        // 추가된 메뉴 , 개수 정보 전달
    }

    private void StartGame() 
    {
        base.CloseUI();
        //메뉴정보 전달
    }

}
