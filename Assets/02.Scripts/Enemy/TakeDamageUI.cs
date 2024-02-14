using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TakeDamageUI : MonoBehaviour
{
    private EnemyHealthSystem _enemyHealthSystem;
    private float _duration = 1.5f;

    private void Awake()
    {
        _enemyHealthSystem = GetComponentInParent<EnemyHealthSystem>();
    }

    public IEnumerator DamageUI(TextMeshProUGUI damageAmountTxt)
    {
        damageAmountTxt.color = new(1, 0, 0, 1);

        damageAmountTxt.text = "-" + _enemyHealthSystem.DamageAmount.ToString();
        damageAmountTxt.transform.DOMoveY(0.5f, _duration, false).SetRelative(true);
        damageAmountTxt.DOFade(0, _duration);
        yield return new WaitForSeconds(_duration);
        damageAmountTxt.gameObject.SetActive(false);
            
    }
}
