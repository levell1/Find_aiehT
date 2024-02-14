using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRespawn : MonoBehaviour
{
    private Enemy _enemy;
    private Vector3 _enemySpawnPoint;
    private float _respawnTime = 0;
    public int RespawnDelay;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemySpawnPoint = transform.position;
        if (SceneManager.GetActiveScene().name=="KJW")
        {
            RespawnDelay = 100;
        }
    }

    private void Update()
    {
        if (_enemy.HealthSystem.IsDead)
        {
             _respawnTime += Time.deltaTime;
            if (_respawnTime > RespawnDelay)
            {
                Respawn();
            }
        }
    }

    private void Respawn()
    {
        _enemy.SetData();
        if (GameManager.Instance.GlobalTimeManager.NightCheck())
        {
            _enemy.EnemyDamage *= 2f;
            _enemy.EnemyMaxHealth *= 2f;
            _enemy.EnemyDropEXP *= 2;
        }

        _respawnTime = 0f;
        _enemy.HealthSystem.IsDead = false;
        _enemy.Collider.enabled = true;

        _enemy.HealthSystem.Health = _enemy.HealthSystem.MaxHealth;

        if (_enemySpawnPoint != null)
        {
            _enemy.transform.position = _enemySpawnPoint;
        }
    }
}
