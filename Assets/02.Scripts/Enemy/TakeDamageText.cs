using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeDamageText : MonoBehaviour
{
    private TakeDamageUI _takeDamageUI;
    public TextMeshProUGUI DamageAmountTxt;
    private Vector3 _baseTransform;

    private Coroutine _coroutine;

    private void Awake()
    {
        _takeDamageUI = GetComponentInParent<TakeDamageUI>();
        _baseTransform = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = _baseTransform;
        if ( _coroutine == null )
        {
            _coroutine = StartCoroutine(_takeDamageUI.DamageUI(DamageAmountTxt));
        }
    }

    private void OnDisable()
    {
        _coroutine = null;
    }
}
