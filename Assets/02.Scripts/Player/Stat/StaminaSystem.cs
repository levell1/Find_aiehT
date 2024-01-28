using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float _targetRegenTime = 0.2f;

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

        _regenTime = 0f;
    }

    public void SetMaxStamina()
    {
        MaxStamina = _playerData.PlayerData.GetPlayerMaxStamina();
        Stamina = MaxStamina;
        OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
    }

    public bool CanUseDash(int dashStamina)
    {
        return Stamina >= dashStamina;
    }

    /// 대쉬시 - 10;
    public void UseDash(int dashStamina)
    {
        if (Stamina == 0 || _dash.IsDash) return;

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
        OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
        Debug.Log(Stamina);
    }


    /// 스태미너 초당 재생력
    public void RegenerateStamina(int regenStamina)
    {
        if (Stamina == 100) return;

        _regenTime += Time.deltaTime;

        if (_regenTime >= _targetRegenTime)
        {
            Stamina = Mathf.Min(Stamina + regenStamina, MaxStamina);
            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
            _regenTime = 0;

            //Debug.Log(_stamina);
            //Debug.Log("재생!");
        }
    }

    public void Healing(int healingAmount)
    {
        if (Stamina < MaxStamina)
        {
            Stamina += healingAmount;

            Stamina = Mathf.Min(Stamina, MaxStamina);

            OnChangeStaminaUI?.Invoke(Stamina, MaxStamina);
        }

    }

}
