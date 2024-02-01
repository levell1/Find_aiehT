using UnityEngine;
public class PlayerHPBar : PlayerStatSlider
{
    [SerializeField] private HealthSystem _healthSystem;

    public new void Awake()
    {
        if (_healthSystem==null)
        {
            _healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        }
        base.Awake();
        _healthSystem.OnChangeHpUI += base.ChangeBar;
    }
}
