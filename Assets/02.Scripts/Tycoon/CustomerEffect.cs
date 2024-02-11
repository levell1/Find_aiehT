using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEffect : MonoBehaviour
{
    [SerializeField] Transform _eatTransform;

    private WaitForSeconds _eatTime = new WaitForSeconds(6f);
    private bool _isEat = false;

    public void PlayEatEffect()
    {
        if (!_isEat)
        {
            GameManager.Instance.EffectManager.CustomerEatFoodEffect(_eatTransform, _eatTime);
            _isEat = true;
        }
    }

    public void PlayGetCoinEffect()
    {
        GameManager.Instance.EffectManager.GetCoinEffect(_eatTransform);
    }
}
