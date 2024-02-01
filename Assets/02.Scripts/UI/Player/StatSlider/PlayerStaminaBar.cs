using UnityEngine;

public class PlayerStaminaBar : PlayerStatSlider
{
    [SerializeField] private StaminaSystem _staminaSystem;

    public new void Awake()
    {
        base.Awake();
        _staminaSystem.OnChangeStaminaUI += base.ChangeBar;
    }
}
