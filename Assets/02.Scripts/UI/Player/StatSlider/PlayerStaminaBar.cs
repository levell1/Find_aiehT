using UnityEngine;

public class PlayerStaminaBar : PlayerStatSlider
{
    [SerializeField] private StaminaSystem _staminaSystem;

    public new void Awake()
    {
        if (_staminaSystem == null)
        {
            _staminaSystem = GameObject.FindWithTag("Player").GetComponent<StaminaSystem>();
        }
        base.Awake();
        _staminaSystem.OnChangeStaminaUI += base.ChangeBar;
    }
}
