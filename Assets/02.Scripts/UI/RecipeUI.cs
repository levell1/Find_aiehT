using UnityEngine;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private GameObject _foodInfoPanel;
    [SerializeField] private FoodSO[] _foodDatas;
    [SerializeField] private InvenRecipeSlot[] _invenRecipeSlot;

    private void OnEnable()
    {
        _foodInfoPanel.SetActive(false);
    }

    private void Start()
    {
        _foodDatas = GameManager.Instance.DataManager.FoodSODatas;

        for (int i = 0; i < _invenRecipeSlot.Length; i++)
        {
            _invenRecipeSlot[i].Index = i;
            _invenRecipeSlot[i].FoodData = _foodDatas[i + 1];
        }
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
