using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : PlayerStatSlider
{
    [SerializeField] private HealthSystem _healthSystem;

    public new void Awake()
    {
        base.Awake();
        _healthSystem.OnChangeHpUI += base.ChangeBar;
    }
}
