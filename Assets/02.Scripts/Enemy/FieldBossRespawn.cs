using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldBossRespawn : MonoBehaviour
{
    private GlobalTimeManager _globalTimeManager;
    private DataManager _dataManager;
    private Enemy _enemy;
    private Vector3 _enemySpawnPoint;

    private void Awake()
    {
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _dataManager = GameManager.Instance.DataManager;
        _enemy = GetComponent<Enemy>();
        _enemySpawnPoint = transform.position;
    }

    private void Start()
    {
        _globalTimeManager.OnBossRespawn += Respawn;
        _globalTimeManager.OnNightCheck += NightEnemyStat;

        DeadCheck();
    }

    private void OnDisable()
    {
        _globalTimeManager.OnBossRespawn -= Respawn;
        _globalTimeManager.OnNightCheck -= NightEnemyStat;
    }

    private void DeadCheck()
    {
        if (!_dataManager.BossDeadCheckDict.ContainsKey(_enemy.Data.EnemyID))
        {
            _dataManager.BossDeadCheckDict.Add(_enemy.Data.EnemyID, _enemy.HealthSystem.IsDead);
        }

        if (_dataManager.BossDeadCheckDict.ContainsKey(_enemy.Data.EnemyID))
        {
            _enemy.HealthSystem.IsDead = _dataManager.BossDeadCheckDict[_enemy.Data.EnemyID];
            if (_enemy.HealthSystem.IsDead)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Respawn()
    {
        _dataManager.BossDeadCheckDict.Remove(_enemy.Data.EnemyID);
    }

    private void NightEnemyStat()
    {
        _enemy.EnemyDamage *= 2f;
        _enemy.EnemyMaxHealth *= 2f;
        _enemy.EnemyDropEXP *= 2;
    }
}