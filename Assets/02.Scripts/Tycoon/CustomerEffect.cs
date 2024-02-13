using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEffect : MonoBehaviour
{
    [SerializeField] Transform _eatTransform;

    private bool _isEat = false;

    private ParticleSystem _eatParticle;
    private ParticleSystem _coinParticle;

    private void Start()
    {
        EffectInit();
    }

    private void EffectInit()
    {
        _eatParticle = GameManager.Instance.EffectManager.CreateCustomerEatParticle(_eatTransform);
        _coinParticle = GameManager.Instance.EffectManager.CreateCustomerCoinParticle(_eatTransform);
    }

    public void PlayEatEffect()
    {
        if (!_isEat)
        {
            _eatParticle.Play();
            GameManager.Instance.EffectManager.StopEatParticle(_eatParticle);
            _isEat = true;
        }
    }

    public void PlayGetCoinEffect()
    {
        _coinParticle.Play();
    }
}
