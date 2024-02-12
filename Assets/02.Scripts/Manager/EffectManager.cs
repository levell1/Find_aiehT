using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPos;

    private WaitForSeconds _healingEffectTime = new WaitForSeconds(2f);
    private WaitForSeconds _levelupEffectTime = new WaitForSeconds(3f);
    private WaitForSeconds _playerAttackEffectTime = new WaitForSeconds(0.3f);

    private GameObject _player;
    private ParticleSystem _levelupObject;
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
    [SerializeField] private ParticleSystem _coinEffect;

    [SerializeField] private GameObject _playerTakeDamageEffect;
    [SerializeField] private GameObject _playerDieEffect;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        _levelupObject = Instantiate(_levelUpEffect, _player.transform);
        _healingObject = Instantiate(_healingEffect, _player.transform);
        _staminaHealingObject = Instantiate(_staminaHealingEffect, _player.transform);
        _playerAttackObject = Instantiate(_playerAttackEffect, _weaponPos.transform);
        _footStepObject = Instantiate(_footStepEffect, _player.transform);
    }

    public void PlayLevelUpEffect()
    {
        _levelupObject.Play();
        StartCoroutine(StopParticle(_levelupObject, _levelupEffectTime));
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

    //TODO: Quest 구현 완료 후 적용
    public void QuestCompleteEffect()
    {
        _questCompleteObject.Play();
        StartCoroutine(StopParticle(_questCompleteObject, _healingEffectTime));
    }

    public void PlayAttackEffect()
    {
        _playerAttackObject.Play();
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

    public void GetCoinEffect(Transform customerEatTransform)
    {
        ParticleSystem coinParticle = Instantiate(_coinEffect, customerEatTransform);
        coinParticle.Play();
    }

    public void PlayerTakeDamageEffect()
    {
        _playerTakeDamageEffect.SetActive(true);
        StartCoroutine(StopEffectObject(_playerTakeDamageEffect, new WaitForSeconds(0.4f)));
    }

    public void PlayerLowHpEffect(bool isLow)
    {
        _playerTakeDamageEffect.SetActive(isLow);
    }
    
    // TODO: 재시작 구현 후 다시 구현
    public void PlayerDieEffect()
    {
        _playerDieEffect.SetActive(true);
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
