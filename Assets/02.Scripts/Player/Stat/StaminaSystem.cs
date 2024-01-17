using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] private float _targetRegenTime = 0.2f;
    
    private PlayerSO _playerData;
    private int _maxStamina;
    private int _stamina;

    private float _regenTime;

    private void Start()
    {
        _playerData = GetComponent<Player>().Data;

        _maxStamina = _playerData.GetPlayerData().GetPlayerMaxStamina();
        _stamina = _maxStamina;

        _regenTime = 0f;
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

        //Debug.Log("스태미너" + _stamina);
    }

    /// 스태미너 초당 재생력
    public void RegenerateStamina(int regenStamina)
    {
        if (_stamina == 100) return;

        _regenTime += Time.deltaTime;

        if (_regenTime >= _targetRegenTime)
        {
            _stamina = Mathf.Min(_stamina + regenStamina, _maxStamina);

            _regenTime = 0;

            Debug.Log(_stamina);
            Debug.Log("재생!");
        }

        

    }

}
