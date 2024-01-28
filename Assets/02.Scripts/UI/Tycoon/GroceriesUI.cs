
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroceriesUI : MonoBehaviour
{
    [Header("재료 개수")]
    [SerializeField] private TMP_Text countText;
    [SerializeField] private TMP_Text needCountText;
    [SerializeField] private TMP_Text perCountText;
    [SerializeField] private Button _decreaseButton;
    [SerializeField] private Button _increaseButton;
    public int MakeFoodCount;
    public int GroceryCount;
    private void Start()
    {
        MakeFoodCount = 1;
        GroceryCount = 4;
        //countText.text = MakeFoodCount.ToString();

        needCountText.text = (MakeFoodCount * GroceryCount).ToString();
        _increaseButton.onClick.AddListener(IncreaseCount);
        _decreaseButton.onClick.AddListener(DecreaseCount);
    }

    private void DecreaseCount()
    {
        MakeFoodCount--;
        countText.text = MakeFoodCount.ToString();
        needCountText.text = (MakeFoodCount * GroceryCount).ToString();
    }
    private void IncreaseCount()
    {
        MakeFoodCount++;
        countText.text = MakeFoodCount.ToString();
        //재료필요개수 들어가기
        needCountText.text = (MakeFoodCount * GroceryCount).ToString();
    }

}
