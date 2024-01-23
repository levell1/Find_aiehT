using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour
{
   [SerializeField] private float _invincibleTime = 3f; // 무적 시간 
    
    private PlayerSO _playerData;
    public float _maxHealth;
    private float _playerDef;

    public float _health;

    

    private bool _isInvincible = false;

    public event Action OnDie;
    public  Action<float,float> OnChangeHpUI;

    public bool IsDead => _health == 0;

    private void Start()
    {
        _playerData = GetComponent<Player>().Data;
        SetMaxHealth();

        _playerDef = _playerData.PlayerData.GetPlayerDef();
        Debug.Log(_maxHealth);
    }

    public void SetMaxHealth() 
    {
        _maxHealth = _playerData.PlayerData.GetPlayerMaxHealth();
        _health = _maxHealth;
        OnChangeHpUI?.Invoke(_health, _maxHealth);
    }

    private float CaculateTotalDamage(float damage)
    {
        float _defPer = _playerDef / (1 + _playerDef);
        float _totalDamage = damage * (1 - _defPer);

        return _totalDamage;
    }


    public void TakeDamage(float damage)
    {
        _playerDef = _playerData.PlayerData.GetPlayerDef();

        if (_isInvincible) return;

        if (_health == 0) return;

       float _totalDamage = CaculateTotalDamage(damage);

        Debug.Log("헬스" + _health);

        _health = Mathf.Max(Mathf.Floor(_health - _totalDamage), 0);
        OnChangeHpUI?.Invoke(_health, _maxHealth);

        if (_health == 0)
            OnDie.Invoke();

        Debug.Log(_health);
        StartCoroutine(InvincibleCooldown());
    }

    private IEnumerator InvincibleCooldown()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
    }


}
