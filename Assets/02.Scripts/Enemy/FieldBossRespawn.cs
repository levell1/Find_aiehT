using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldBossRespawn : MonoBehaviour
{
    private GlobalTimeManager _globalTimeManager;
    private DataManager _dataManager;
    private Enemy _enemy;

    private void Awake()
    {
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _dataManager = GameManager.Instance.DataManager;
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _globalTimeManager.OnBossRespawn += Respawn;
        _globalTimeManager.OnNightCheck += NightEnemyStat;
        _enemy.HealthSystem.OnDie += DeadEnemy;

        DeadCheck();
    }

    private void OnDisable()
    {
        _globalTimeManager.OnNightCheck -= NightEnemyStat;
        _enemy.HealthSystem.OnDie -= DeadEnemy;
    }

    private void DeadEnemy()
    {
        _dataManager.AddBoss(_enemy.Data.EnemyID, _enemy.HealthSystem.IsDead);
    }

    private void DeadCheck()
    {
        if (_dataManager.BossDeadCheckDict.ContainsKey(_enemy.Data.EnemyID))
        {
            _enemy.HealthSystem.IsDead = _dataManager.BossDeadCheckDict[_enemy.Data.EnemyID];

            if (_enemy.HealthSystem.IsDead)
            {
                if (SceneManager.GetActiveScene().name == SceneName.DungeonScene)
                {
                    _enemy.HealthSystem.IsDead = false;
                }
                else
                {
                    gameObject.SetActive(false);
                }
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