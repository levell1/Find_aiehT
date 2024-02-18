using UnityEngine;

public class PlayerStaminaBar : PlayerStatSlider
{
    private StaminaSystem _staminaSystem;
    private PlayerExpSystem _playerExpSystem;

    public new void Awake()
    {
        if (_staminaSystem == null)
        {
            _staminaSystem = GameManager.Instance.Player.GetComponent<StaminaSystem>();
            _playerExpSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();
        }

        base.Awake();
        _staminaSystem.OnChangeStaminaUI += base.ChangeBar;
        _playerExpSystem.OnChangeSpUI += base.ChangeBar;
    }
}
