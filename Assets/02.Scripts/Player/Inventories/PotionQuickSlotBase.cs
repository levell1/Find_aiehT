using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PotionQuickSlotBase : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    public Image PotionImage;
    public TMP_Text PotionQuantity;

    private int _quantity;


    protected virtual void Start()
    {
        string potionSpritePath = GetPotionSpritePath();

        SetDefaultSprite(potionSpritePath);
    }

    protected abstract string GetPotionSpritePath();

    protected void SetDefaultSprite(string spritePath)
    {
        Sprite defaultSprite = GameManager.instance.ResourceManager.Load<Sprite>(spritePath);

        if (defaultSprite != null)
        {
            PotionImage.sprite = defaultSprite;
        }

    }

    public void ShowPotionToQuickslot(PotionSO data, int quantity)
    {
        PotionSO = data;
        _quantity = quantity;

        PotionQuantity.text = quantity.ToString();
        PotionImage.sprite = data.sprite;

    }

    protected virtual void UpdateUI(int quantity) 
    {
        _quantity = quantity;
        PotionQuantity.text = _quantity.ToString();

    }

    
}