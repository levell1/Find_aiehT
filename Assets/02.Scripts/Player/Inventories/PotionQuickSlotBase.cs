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

        PotionQuantity.text = quantity.ToString();
        PotionImage.sprite = data.sprite;

    }

    
}
