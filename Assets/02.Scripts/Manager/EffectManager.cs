using System.Collections;
using UnityEngine;


public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPos;

    private WaitForSeconds _healingEffectTime = new WaitForSeconds(2f);
    private WaitForSeconds _levelupEffectTime = new WaitForSeconds(3f);
    private WaitForSeconds _eatEffectTime = new WaitForSeconds(6f);

    private WaitForSeconds _greenPigEffect = new WaitForSeconds(4.5f);
    private GameObject _player;
    private ParticleSystem _levelupObject;
    private ParticleSystem _healingObject;
    private ParticleSystem _staminaHealingObject;
    private ParticleSystem _playerAttackObject;
    private ParticleSystem _mainQuestCompleteObject;
    private ParticleSystem _questCompleteObject;
    private ParticleSystem _footStepObject;

    [Header("Effect Prefabs")]
    [SerializeField] private ParticleSystem _levelUpEffect;
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private ParticleSystem _staminaHealingEffect;
    [SerializeField] private ParticleSystem _playerAttackEffect;
    [SerializeField] private ParticleSystem _mainQuestCompleteEffect;
    [SerializeField] private ParticleSystem _questCompleteEffect;
    [SerializeField] private ParticleSystem _footStepEffect;
    [SerializeField] private ParticleSystem _eatFoodEffect;
    [SerializeField] private ParticleSystem _coinEffect;
    [SerializeField] public ParticleSystem GreenPigEffect;

    [SerializeField] private GameObject _playerTakeDamageEffect;

    private Coroutine _coroutine;
    private void Start()
    {
        _player = GameManager.Instance.Player;

        _levelupObject = Instantiate(_levelUpEffect, _player.transform);
        _healingObject = Instantiate(_healingEffect, _player.transform);
        _staminaHealingObject = Instantiate(_staminaHealingEffect, _player.transform);
        _playerAttackObject = Instantiate(_playerAttackEffect, _weaponPos.transform);
        _mainQuestCompleteObject = Instantiate(_mainQuestCompleteEffect, _player.transform);
        _questCompleteObject = Instantiate(_questCompleteEffect, _player.transform);
        _footStepObject = Instantiate(_footStepEffect, _player.transform);
    }

    public void PlayLevelUpEffect()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.LevelUp);
        _levelupObject.Play();
        StartCoroutine(StopParticle(_levelupObject, _levelupEffectTime));
    }
    
    public void PlayHealingEffect()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Heal);
        _healingObject.Play();
        StartCoroutine(StopParticle(_healingObject, _healingEffectTime));
    }

    public void PlayStaminaEffect()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Stamina);
        _staminaHealingObject.Play();
        StartCoroutine(StopParticle(_staminaHealingObject, _healingEffectTime));
    }

    public void QuestCompleteEffect()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Quest);
        _questCompleteObject.Play();
        StartCoroutine(StopParticle(_questCompleteObject, _healingEffectTime));
    }

    public void MainQuestCompleteEffect()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Quest);
        _mainQuestCompleteObject.Play();
        StartCoroutine(StopParticle(_mainQuestCompleteObject, _healingEffectTime));
    }

    public void GreenPigLevitate()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Levitate);
        GreenPigEffect.Play();
        StartCoroutine(StopParticle(GreenPigEffect, _greenPigEffect));

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

    public ParticleSystem CreateCustomerEatParticle(Transform customerEatTransform)
    {
        return Instantiate(_eatFoodEffect, customerEatTransform);
    }

    public ParticleSystem CreateCustomerCoinParticle(Transform customerEatTransform)
    {
        return Instantiate(_coinEffect, customerEatTransform);
    }

    public ParticleSystem CreateGreenPigLevitate(Transform greenpig)
    {
        return Instantiate(GreenPigEffect, greenpig);
    }

    public void StopEatParticle(ParticleSystem particle)
    {
        StartCoroutine(StopParticle(particle, _eatEffectTime));
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
