using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPos;

    private WaitForSeconds _healingEffectTime = new WaitForSeconds(2f);
    private WaitForSeconds _playerAttackEffectTime = new WaitForSeconds(0.3f);

    private GameObject _player;
    private ParticleSystem _healingObject;
    private ParticleSystem _staminaHealingObject;
    private ParticleSystem _playerAttackObject;
    private ParticleSystem _footStepObject;

    [Header("Effect Prefabs")]
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private ParticleSystem _staminaHealingEffect;
    [SerializeField] private ParticleSystem _playerAttackEffect;
    [SerializeField] private ParticleSystem _footStepEffect;
    [SerializeField] private ParticleSystem _eatFoodEffect;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        _healingObject = Instantiate(_healingEffect, _player.transform);
        _staminaHealingObject = Instantiate(_staminaHealingEffect, _player.transform);
        _playerAttackObject = Instantiate(_playerAttackEffect, _weaponPos.transform);
        _footStepObject = Instantiate(_footStepEffect, _player.transform);
    }

    public void PlayLevelUpEffect()
    {
        ParticleSystem levelupEffectObject = Instantiate(_levelUpEffect, _player.transform);
        Destroy(levelupEffectObject, 5f);
    }
    
    public void PlayHealingEffect()
    {
        _healingObject.Play();
        StartCoroutine(TurnOffEffect(_healingObject, _healingEffectTime));
    }

    public void PlayStaminaEffect()
    {
        _staminaHealingObject.Play();
        StartCoroutine(TurnOffEffect(_staminaHealingObject, _healingEffectTime));
    }

    public void PlayAttackEffect()
    {
        _playerAttackObject.Play();
        StartCoroutine(TurnOffEffect(_playerAttackObject, _playerAttackEffectTime));
    }

    public void PlayFootStepEffect()
    {
        _footStepObject.Play();
    }

    public void StopFootStepEffect()
    {
        _footStepObject.Stop();
    }

    public void CustomerEatFoodEffect(Transform customerEatTransform, WaitForSeconds eatTime)
    {
        ParticleSystem eatParticle = Instantiate(_eatFoodEffect, customerEatTransform);
        StartCoroutine(TurnOffEffect(eatParticle, eatTime));
    }

    #region Coroutine

    IEnumerator TurnOffEffect(ParticleSystem effectObject, WaitForSeconds effectTime)
    {
        yield return effectTime;
        effectObject.Stop();
    }

    #endregion
}
