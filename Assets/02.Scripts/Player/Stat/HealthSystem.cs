using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealthSystem : MonoBehaviour
{
    private Player _player;
    private PlayerSO _playerData;
    private int _maxHealth;
    private int _playerDef;

    private int _health;

    private float knockbackForce = 5f;
    private bool _isInvincible = false;
    private float _invincibleTime = 3f; // 무적 시간 

    public event Action OnDie;

    public bool IsDead => _health == 0;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerData = GetComponent<Player>().Data;

        _maxHealth = _playerData.GetPlayerData().GetPlayerMaxHealth();
        _health = _maxHealth;

        _playerDef = _playerData.GetPlayerData().GetPlayerDef();

        Debug.Log(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (_isInvincible) return;

        if (_health == 0) return;
        //TODO DEF
        _health = Mathf.Max((_health + _playerDef) - damage, 0);

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
