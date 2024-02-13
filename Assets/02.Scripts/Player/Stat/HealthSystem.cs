using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour
{
   [SerializeField] private float _invincibleTime = 3f; // 무적 시간 
    
    private PlayerSO _playerData;
    public float MaxHealth;
    private float _playerDef;

    public float Health;
    private const int _dangerHealth = 30;

    [SerializeField] EquipmentDatas _equipmentDatas;

    private bool _isInvincible = false;

    public event Action OnDie;
    public  Action<float,float> OnChangeHpUI;

    public bool IsDead => Health == 0;

    private void Awake()
    {
        _equipmentDatas = GetComponent<EquipmentDatas>();
        _playerData = GetComponent<Player>().Data;
    }
    private void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth() 
    {
        MaxHealth = _playerData.PlayerData.GetPlayerMaxHealth()+ _equipmentDatas.SumHealth;
        Health = MaxHealth;
        OnChangeHpUI?.Invoke(Health, MaxHealth);
    }

    private float CaculateTotalDamage(float damage)
    {
        float _defPer = 0.02f * _playerDef / (1 + 0.02f * _playerDef); // 1/2 
        float _totalDamage = damage * (1 - _defPer);   //  

        return _totalDamage;
    }


    public void TakeDamage(float damage)
    {
        _playerDef = _playerData.PlayerData.GetPlayerDef() + _equipmentDatas.SumDef;
        if (_isInvincible) return;

        if (Health == 0) return;

        float _totalDamage = CaculateTotalDamage(damage);

        Health = Mathf.Max(Mathf.Floor(Health - _totalDamage), 0);
        OnChangeHpUI?.Invoke(Health, MaxHealth);
        
        if (Health < _dangerHealth)
            GameManager.Instance.EffectManager.PlayerLowHpEffect(true);
        else
            GameManager.Instance.EffectManager.PlayerTakeDamageEffect();

        if (Health == 0)
        {
            OnDie.Invoke();
            GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
            GameManager.Instance.EffectManager.PlayerDieEffect();
        }

        StartCoroutine(InvincibleCooldown());
    }

    private IEnumerator InvincibleCooldown()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
    }

    public void Healing(int healingAmount)
    {
        if(Health < MaxHealth)
        {
            Health += healingAmount;

            Health = Mathf.Min(Health, MaxHealth);

            OnChangeHpUI?.Invoke(Health, MaxHealth);

            GameManager.Instance.EffectManager.PlayHealingEffect();

            if(Health >= _dangerHealth)
                GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        }
    }

}
