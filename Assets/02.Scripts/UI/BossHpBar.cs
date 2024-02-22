using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : PlayerBaseSlider
{
    [SerializeField] private Image _leftImage;
    [SerializeField] private Image _rightImage;
    [SerializeField] private BossHealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem.OnChangeHpUI += ChangeBar;
    }

    protected override void ChangeBar(float currentValue, float maxValue)
    {
        base.ChangeBar(currentValue, maxValue);
        if (_slider.value == 1)
        {
            _rightImage.color = new Color32(255, 50, 50, 255);
        }
        else if (_slider.value != 1)
        {
            _rightImage.color = new Color32(255, 50, 50, 60);
        }

        if (_slider.value == 0)
        {
            _leftImage.color = new Color32(255, 50, 50, 60);
        }
        else if (_slider.value != 0)
        {
            _leftImage.color = new Color32(255, 50, 50, 255);
        }
    }
    
}
