using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PotionQuickSlotBase : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    public Image PotionImage;
    public TMP_Text PotionQuantity;

    public int Quantity;


    protected virtual void Start()
    {
        string potionSpritePath = GetPotionSpritePath();

        SetDefaultSprite(potionSpritePath);
    }

    protected abstract string GetPotionSpritePath();

    protected void SetDefaultSprite(string spritePath)
    {
        Sprite defaultSprite = GameManager.Instance.ResourceManager.Load<Sprite>(spritePath);

        if (defaultSprite != null)
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

    }

    protected virtual void UpdateUI(int quantity) 
    {
        Quantity = quantity;
        PotionQuantity.text = Quantity.ToString();

    }

    
}
