using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PotionQuickSlotBase : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    // protected Player _player;
    public Image PotionImage;
    public TMP_Text PotionQuantity;

    public int Quantity;


    protected virtual void Start()
    {
        string potionSpritePath = GetPotionSpritePath();

        SetDefaultSprite(potionSpritePath);

        //_player = GameManager.Instance.Player.GetComponent<Player>();
    }

    protected abstract string GetPotionSpritePath();

    protected void SetDefaultSprite(string spritePath)
    {
        Sprite defaultSprite = GameManager.Instance.ResourceManager.Load<Sprite>(spritePath);

        if (defaultSprite != null && GameManager.Instance.GameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            PotionImage.sprite = defaultSprite;
        }

    }
    public virtual void DefaultPotionInit(PotionSO data)
    {
        PotionSO = data;
    }

    public void ShowPotionToQuickslot(PotionSO data, int quantity)
    {
        PotionSO = data;
        Quantity = quantity;

        PotionQuantity.text = quantity.ToString();
        PotionImage.sprite = data.sprite;

        QuickSlotUpdateUI(quantity);
    }

    protected virtual void QuickSlotUpdateUI(int quantity) 
    {
        Quantity = quantity;
        PotionQuantity.text = Quantity.ToString();

    }

    
}
