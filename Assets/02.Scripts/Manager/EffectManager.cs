using System;
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
    private ParticleSystem _questCompleteObject;
    private ParticleSystem _footStepObject;

    [Header("Effect Prefabs")]
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private ParticleSystem _staminaHealingEffect;
    [SerializeField] private ParticleSystem _playerAttackEffect;
    [SerializeField] private ParticleSystem _questCompleteEffect;
    [SerializeField] private ParticleSystem _footStepEffect;
    [SerializeField] private ParticleSystem _eatFoodEffect;

    [SerializeField] private GameObject _playerTakeDamageEffect;

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
        StartCoroutine(StopParticle(_healingObject, _healingEffectTime));
    }

    public void PlayStaminaEffect()
    {
        _staminaHealingObject.Play();
        StartCoroutine(StopParticle(_staminaHealingObject, _healingEffectTime));
    }

    public void PlayAttackEffect()
    {
        _playerAttackObject.Play();
        StartCoroutine(StopParticle(_playerAttackObject, _playerAttackEffectTime));
    }

    //TODO: Quest 구현 완료 후 적용
    public void QuestCompleteEffect()
    {
        _questCompleteObject.Play();
        StartCoroutine(StopParticle(_questCompleteObject, _healingEffectTime));
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
        StartCoroutine(StopParticle(eatParticle, eatTime));
    }

    public void PlayerTakeDamageEffect()
    {
        _playerTakeDamageEffect.SetActive(true);
        StartCoroutine(StopEffectObject(_playerTakeDamageEffect, new WaitForSeconds(0.4f)));
    }

    public void PlayerLowHpEffect()
    {
        _playerTakeDamageEffect.SetActive(true);
    }
    
    public void PlayerGettingBetterEffect()
    {
        _playerTakeDamageEffect.SetActive(false);
    }

    #region Coroutine

    IEnumerator StopParticle(ParticleSystem effectObject, WaitForSeconds effectTime)
    {
        yield return effectTime;
        effectObject.Stop();
    }

    IEnumerator StopEffectObject(GameObject effectObject, WaitForSeconds effectTime)
    {
        yield return effectTime;
        effectObject.SetActive(false);
    }

    #endregion
}
