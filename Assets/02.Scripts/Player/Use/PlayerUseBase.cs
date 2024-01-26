using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseBase : MonoBehaviour
{
    protected Player _player;
    protected PotionSO _potion;
    protected int _healingAmount;
    protected int _quantity;

    public event Action<int> OnPotionUsed;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public virtual void Potion(PotionSO data, int quantity)
    {
        _potion = data;
        _healingAmount = data.HealingAmount;
        _quantity = quantity;
    }

    public virtual void UsePotion()
    {
        if(_potion == null || _quantity <= 0)
        {
            Debug.Log("포션을 장착해주세요");
        }
        else
        {
            Healing();
            _quantity--;

            OnPotionUsed?.Invoke(_quantity);
        }
    }

    protected virtual void Healing() {}

}
