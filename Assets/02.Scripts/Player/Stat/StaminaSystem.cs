using System;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float _targetRegenTime = 1f;

    private PlayerSO _playerData;
    private DashForceReceiver _dash;
    public float MaxStamina;
    public float Stamina;

    public  Action<float, float> OnChangeStaminaUI;

    private float _regenTime;

    private void Start()
    {
        _playerData = GetComponent<Player>().Data;
        _dash = GetComponent<Player>().DashForceReceiver;

        SetMaxStamina();
        SetCurStamina();

        _regenTime = 0f;
    }

    public void SetMaxStamina()
    {
        MaxStamina = _playerData.PlayerData.PlayerMaxStamina;
    }

    public void SetCurStamina()
    {
        GameStateManager _gameStateManager = GameManager.Instance.GameStateManager;

        if (_gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            Stamina = MaxStamina;
            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
        }
        else if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            float loadStamina = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveStamina;
            Stamina = loadStamina;
            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
        }
    }

    public bool CanUseDash(int dashStamina)
    {
        return Stamina >= dashStamina;
    }

    public void UseDash(int dashStamina)
    {
        if (Stamina == 0 || _dash.IsDash) return;
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Dash);
        Stamina = Mathf.Max(Stamina - dashStamina, 0);
        OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
    }

    public bool CanUseSkill(int skillCost)
    {
        return Stamina >= skillCost;
    }

    public void UseSkill(int skillStamina)
    {
        if (Stamina == 0) return;
        Stamina = Mathf.Max(Stamina - skillStamina, 0);
        OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);;
    }


    public void RegenerateStamina(int regenStamina)
    {
        if (Stamina == 100) return;

        _regenTime += Time.deltaTime;

        if (_regenTime >= _targetRegenTime)
        {
            Stamina = Mathf.Min(Stamina + regenStamina, MaxStamina);
            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
            _regenTime = 0;
        }
    }
    public void ReduceStamina(float reduceStamina, float delayTime)
    {
        if (Stamina < 50) return;
        _regenTime += Time.deltaTime;

        if (_regenTime >= delayTime)
        {
            Stamina -= reduceStamina;
            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
            _regenTime = 0;
        }
    }

    public void Healing(float healingAmount)
    {
        if (Stamina < MaxStamina)
        {
            Stamina += Mathf.Ceil(MaxStamina * (healingAmount * 0.01f));

            Stamina = Mathf.Min(Stamina, MaxStamina);

            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);

            GameManager.Instance.EffectManager.PlayStaminaEffect();
        }

    }

}
