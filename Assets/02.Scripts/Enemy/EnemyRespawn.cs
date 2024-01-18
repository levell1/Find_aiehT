using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _respawnTime = 0f;
        _enemy.Animator.SetBool("Die", false);
        _enemy.HealthSystem.IsDead = false;
        _enemy.Collider.enabled = true;
        _enemy.enabled = true;
        _enemy.HealthSystem._health = _enemy.HealthSystem._maxHealth;

        if (_enemySpawnPoint != null)
        {
            _enemy.transform.position = _enemySpawnPoint;
        }
    }
}
