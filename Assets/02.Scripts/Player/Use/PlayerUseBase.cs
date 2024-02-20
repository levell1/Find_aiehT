using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUseBase : MonoBehaviour
{
    protected Player _player;
    protected PotionSO _potion;
    protected int _healingAmount;
    protected int _quantity;
    [SerializeField] protected Image _coolTimeImage;
    protected CoolTimeManager _coolTimeManager;

    public float CoolTime = 3f;
    protected bool _isCoolTime = false;

    public event Action<int> OnPotionUsed;

    protected virtual void Start()
    {
        _player = GetComponent<Player>();
        _coolTimeManager = GameManager.Instance.CoolTimeManger;
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.UIManager.PopupDic[UIName.InventoryUI].SetActive(true); 
        
        GameManager.Instance.UIManager.PopupDic[UIName.InventoryUI].SetActive(false);
    }

    public virtual void Potion(PotionSO data, int quantity)
    {
        _potion = data;
        _healingAmount = data.HealingAmount;
        _quantity = quantity;
    }

    public virtual void UsePotion()
    {
        if (_isCoolTime)
            return;

        if(_potion == null || _quantity <= 0)
        {
            Debug.Log("포션을 장착해주세요");
        }
        else
        {
            CoolTime = 3f;

            Healing();
            _quantity--;

            OnPotionUsed?.Invoke(_quantity);

            StartCoolTime();
        }
    }

    protected virtual void StartCoolTime()
    {
        if (!_isCoolTime)
        {
            _isCoolTime = true;
        }
    }

    protected virtual void Healing() {}

    protected virtual void HandleCoolTimeFinish()
    {
        _isCoolTime = false;
    }

}
