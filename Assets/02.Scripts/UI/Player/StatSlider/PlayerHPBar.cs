using UnityEngine;
public class PlayerHPBar : PlayerStatSlider
{
    private HealthSystem _healthSystem;
    private PlayerExpSystem _playerExpSystem;

    public new void Awake()
    {
        if (_healthSystem==null)
        {
            _healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
            _playerExpSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();
        }
        base.Awake();
        _healthSystem.OnChangeHpUI += base.ChangeBar;
        _playerExpSystem.OnChangeHpUI += base.ChangeBar;
    }
}
