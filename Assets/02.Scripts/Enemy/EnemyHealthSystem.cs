using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    private Enemy _enemy;
    private EnemySO _enemySO;
    public bool Hit;
    public int _maxHealth;
    public int _health;
    //TODO 시간지나면 UI 안보이게 하기
    public Canvas HpCanvas;
    public Image HpBar;
    public event Action OnDie;

    public bool IsDead;

    private Camera _camera;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemySO = GetComponent<Enemy>().Data;

        _maxHealth = _enemySO.MaxHealth;
        _health = _maxHealth;

        _camera = Camera.main;
    }

    private void Update()
    {
        HpCanvas.transform.LookAt(HpCanvas.transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.down);
        HpBar.fillAmount = (float)_health / _maxHealth;
    }


    public void TakeDamage(int damage)
    {
        Hit = true;
        if (_health == 0) return;
        _health = Mathf.Max(_health  - damage, 0);

        _enemy._stateMachine.ChangeState(_enemy._stateMachine.ChasingState);

        if (_health == 0)
        {
            IsDead = true;
            OnDie.Invoke();
        }
    }
}
