using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUseBase : MonoBehaviour
{
    protected Player _player;
    protected PotionSO _potion;
    protected float _healingAmount;
    protected int _quantity;
    [SerializeField] protected Image _coolTimeImage;
    protected CoolTimeManager _coolTimeManager;

    public float CoolTime;
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
            //TODO 포션부족 
        }
        else
        {
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
