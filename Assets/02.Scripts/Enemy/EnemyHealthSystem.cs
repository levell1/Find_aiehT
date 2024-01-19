using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private EnemySO _enemySO;
    public int _maxHealth;
    public int _health;

    public event Action OnDie;

    public bool IsDead;

    private void Start()
    {
        _enemySO = GetComponent<Enemy>().Data;

        _maxHealth = _enemySO.MaxHealth;
        _health = _maxHealth;
    }

    private void Update()
    {
        //TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        if (_health <= 0) return;
        _health -= damage;

        if (_health <= 0)
        {
            IsDead = true;
            OnDie.Invoke();
        }

        //Debug.Log(_health);

    }
}
