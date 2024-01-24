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
    public float MaxHealth;
    public float Health;
    public Canvas HpCanvas;
    public Image HpBar;
    public bool Hit;
    public int HitCool;

    public event Action OnDie;

    public bool IsDead;

    private Camera _camera;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemySO = GetComponent<Enemy>().Data;

        MaxHealth = _enemy.EnemyMaxHealth;
        Health = MaxHealth;
        HpCanvas.gameObject.SetActive(false);
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Hit)
        {
            HpCanvas.gameObject.SetActive(true);
            //카메라 방향으로 캔버스 돌리기
            HpCanvas.transform.LookAt(HpCanvas.transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.down);

            StartCoroutine(HitCancel());
        }
        else
        {
            HpCanvas.gameObject.SetActive(false);
        }

        HpBar.fillAmount = Health / MaxHealth;
    }


    public void TakeDamage(float damage)
    {
        Hit = true;
        if (Health == 0) return;
        Health = Mathf.Max(Health - damage, 0);

        _enemy._stateMachine.ChangeState(_enemy._stateMachine.ChasingState);

        if (Health == 0)
        {
            IsDead = true;
            OnDie.Invoke();
        }
    }

    private IEnumerator HitCancel()
    {
        yield return new WaitForSeconds(HitCool); //해당시간동안 플레이어를 추격한다.
        Hit = false;
    }
}
