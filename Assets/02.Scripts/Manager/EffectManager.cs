using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelUpEffect;
    [SerializeField] private GameObject _healingEffect;
    [SerializeField] private GameObject _playerAttackEffect;
    //[SerializeField] private GameObject _footStepEffect;
    
    [SerializeField] private GameObject _weaponPos;

    private GameObject _player;
    private GameObject _healingObject;

    private WaitForSeconds _healingEffectTime = new WaitForSeconds(1f);
    private WaitForSeconds _playerAttackEffectTime = new WaitForSeconds(0.5f);

    private void Start()
    {
        _player = GameManager.Instance.Player;

        _healingObject = Instantiate(_healingEffect, _player.transform);
        _healingObject.SetActive(false);

        _playerAttackEffect = Instantiate(_playerAttackEffect, _weaponPos.transform);
        _playerAttackEffect.transform.localScale *= 0.3f;
        _playerAttackEffect.SetActive(false);
    }

    public void PlayLevelUpEffect()
    {
        GameObject levelupEffectObject = Instantiate(_levelUpEffect, _player.transform);
        Destroy(levelupEffectObject, 5f);
    }
    
    public void PlayHealingEffect()
    {
        _healingObject.SetActive(true);
        StartCoroutine(TurnOffEffect(_healingObject, _healingEffectTime));
    }

    public void PlayAttackEffect()
    {
        _playerAttackEffect.SetActive(true);
        StartCoroutine(TurnOffEffect(_playerAttackEffect, _playerAttackEffectTime));
    }

    public void PlayFootStepEffect()
    {
        // Object Pool
        
    }

    #region Coroutine

    IEnumerator TurnOffEffect(GameObject effectObject, WaitForSeconds effectTime)
    {
        yield return effectTime;
        effectObject.SetActive(false);
    }

    #endregion
}
