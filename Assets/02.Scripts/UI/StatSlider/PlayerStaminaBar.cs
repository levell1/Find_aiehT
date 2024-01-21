using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : PlayerStatSlider
{
    [SerializeField] private StaminaSystem _staminaSystem;

    public new void Awake()
    {
        base.Awake();
        _staminaSystem.OnChangeStaminaUI += base.ChangeBar;
    }
}
