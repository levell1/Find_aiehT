using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float _targetRegenTime = 0.2f;
    
    private PlayerSO _playerData;
    private float _maxStamina;
    private float _stamina;

    public  Action<float, float> OnChangeStaminaUI;

    private float _regenTime;

    private void Start()
    {
        _playerData = GetComponent<Player>().Data;
        SetMaxStamina();

        _regenTime = 0f;
    }

    public void SetMaxStamina()
    {
        _maxStamina = _playerData.PlayerData.GetPlayerMaxStamina();
        _stamina = _maxStamina;
        OnChangeStaminaUI?.Invoke(_stamina, _maxStamina);
    }

    public bool CanUseDash(int dashStamina)
    {
        return _stamina >= dashStamina;
    }

    /// 대쉬시 - 10;
    public void UseDash(int dashStamina)
    {
        if (_stamina == 0) return;
        _stamina = Mathf.Max(_stamina - dashStamina, 0);
        OnChangeStaminaUI?.Invoke(_stamina, _maxStamina);
        //Debug.Log("스태미너" + _stamina);
    }

    public bool CanUseSkill(int skillCost)
    {
        return _stamina >= skillCost;
    }

    public void UseSkill(int skillStamina)
    {
        if (_stamina == 0) return;
        _stamina = Mathf.Max(_stamina - skillStamina, 0);
        OnChangeStaminaUI?.Invoke(_stamina, _maxStamina);
        Debug.Log(_stamina);
    }


    /// 스태미너 초당 재생력
    public void RegenerateStamina(int regenStamina)
    {
        if (_stamina == 100) return;

        _regenTime += Time.deltaTime;

        if (_regenTime >= _targetRegenTime)
        {
            _stamina = Mathf.Min(_stamina + regenStamina, _maxStamina);
            OnChangeStaminaUI?.Invoke(_stamina, _maxStamina);
            _regenTime = 0;

            //Debug.Log(_stamina);
            //Debug.Log("재생!");
        }
    }

}
