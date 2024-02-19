using System;
using System.Collections;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   [SerializeField] private float _invincibleTime = 3f; // 무적 시간 
    
    private PlayerSO _playerData;
    public float MaxHealth;
    private float _playerDef;

    public float Health;
    private const int _dangerHealth = 30;

    [SerializeField] EquipmentDatas _equipmentDatas;

    private bool _isInvincible = false;

    public event Action OnDie;
    public  Action<float,float> OnChangeHpUI;

    public bool IsDead => Health == 0;

    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _equipmentDatas = GetComponent<EquipmentDatas>();
        _playerData = GetComponent<Player>().Data;
        _gameStateManager = GameManager.Instance.GameStateManager;
    }
    private void OnEnable()
    {
        SetMaxHealth();
        SetCurHealth();
    }


    public void SetMaxHealth() 
    {
        MaxHealth = _playerData.PlayerData.GetPlayerMaxHealth() + _equipmentDatas.SumHealth;
    }

    // 로드게임 => Health = 저장된 체력
    // 새 게임 => Health = MaxHealth
    // 새게임인지 이어하기인지 알수있는 방법
    public void SetCurHealth()
    {
        if(_gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            Health = MaxHealth;
            OnChangeHpUI?.Invoke(Health, MaxHealth);
        }
        else if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            float loadHealth = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveHealth;
            Health = loadHealth;
            OnChangeHpUI?.Invoke(Health, MaxHealth);
        }
    }

    private float CaculateTotalDamage(float damage)
    {
        float _defPer = 0.02f * _playerDef / (1 + 0.02f * _playerDef); // 1/2 
        float _totalDamage = damage * (1 - _defPer);   //  

        return _totalDamage;
    }


    public void TakeDamage(float damage)
    {
        _playerDef = _playerData.PlayerData.GetPlayerDef() + _equipmentDatas.SumDef;
        if (_isInvincible) return;

        if (Health == 0) return;

        float _totalDamage = CaculateTotalDamage(damage);

        Health = Mathf.Max(Mathf.Floor(Health - _totalDamage), 0);
        OnChangeHpUI?.Invoke(Health, MaxHealth);
        
        if (Health < _dangerHealth)
            GameManager.Instance.EffectManager.PlayerLowHpEffect(true);
        else
            GameManager.Instance.EffectManager.PlayerTakeDamageEffect();

        if (Health == 0)
        {
            OnDie.Invoke();
            GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        }

        StartCoroutine(InvincibleCooldown());
    }

    private IEnumerator InvincibleCooldown()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleTime);
        _isInvincible = false;
    }

    public void Healing(int healingAmount)
    {
        if(Health < MaxHealth)
        {
            Health += healingAmount;

            Health = Mathf.Min(Health, MaxHealth);

            OnChangeHpUI?.Invoke(Health, MaxHealth);

            GameManager.Instance.EffectManager.PlayHealingEffect();

            if(Health >= _dangerHealth)
                GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        }
    }

}
