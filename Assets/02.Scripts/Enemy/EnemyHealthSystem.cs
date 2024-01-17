using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private EnemySO _enemySO;
    private int _maxHealth;
    private int _health;

    public event Action OnDie;

    //public bool IsDead => _health == 0;

    private void Start()
    {
        _enemySO = GetComponent<Enemy>().Data;

        _maxHealth = _enemySO.MaxHealth;
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_health <= 0)
            OnDie.Invoke();

        if (_health <= 0) return;
        _health -= damage;
    }
}
