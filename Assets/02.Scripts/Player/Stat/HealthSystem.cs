using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   //[SerializeField] private float _invincibleTime = 0.5f; // 무적 시간 
    
    private Player _player;
    private PlayerSO _playerData;
    public float MaxHealth;
    private float _playerDef;

    public float Health;
    private const int _dangerHealth = 30;

    [SerializeField] EquipmentDatas _equipmentDatas;

    private bool _isInvincible = false;

    public event Action OnDie;
    public  Action<float,float> OnChangeHpUI;

    public bool IsDead = false;

    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _equipmentDatas = GetComponent<EquipmentDatas>();
        _playerData = GetComponent<Player>().Data;
        _gameStateManager = GameManager.Instance.GameStateManager;
    }
    private void Start()
    {
        SetMaxHealth();
        SetCurHealth();
    }


    public void SetMaxHealth() 
    {
        MaxHealth = _playerData.PlayerData.PlayerMaxHealth + _equipmentDatas.SumHealth;
        OnChangeHpUI?.Invoke(Health, MaxHealth);
    }


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
        _playerDef = _playerData.PlayerData.PlayerDef + _equipmentDatas.SumDef;
        if (_isInvincible) return;

        if (Health == 0) return;

        float _totalDamage = CaculateTotalDamage(damage);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Hit,Vector3.zero,0.3f);

        Health = Mathf.Max(Mathf.Floor(Health - _totalDamage), 0);
        OnChangeHpUI?.Invoke(Health, MaxHealth);
        
        if (Health < _dangerHealth)
            GameManager.Instance.EffectManager.PlayerLowHpEffect(true);
        else
            GameManager.Instance.EffectManager.PlayerTakeDamageEffect();

        if (Health == 0)
        {
            IsDead = true;
            OnDie.Invoke();
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Dead);
            GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        }

        // StartCoroutine(InvincibleCooldown());

    }

    // 무적
    //private IEnumerator InvincibleCooldown()
    //{
    //    _isInvincible = true;
    //    yield return new WaitForSeconds(_invincibleTime);
    //    _isInvincible = false;
    //}

    public void Healing(float healingAmount)
    {
        if(Health < MaxHealth)
        {

            Health += Mathf.Ceil(MaxHealth * (healingAmount * 0.01f));

            Health = Mathf.Min(Health, MaxHealth);

            OnChangeHpUI?.Invoke(Health, MaxHealth);

            GameManager.Instance.EffectManager.PlayHealingEffect();

            if(Health >= _dangerHealth)
                GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        }
    }

    public void Respawn()
    {
        IsDead = false;
        Health = MaxHealth;
        OnChangeHpUI?.Invoke(Health, MaxHealth);
        _player.enabled = true;
    }

}
