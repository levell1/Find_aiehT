using UnityEngine;
using UnityEngine.UI;

public class PlayerStatSlider : PlayerBaseSlider
{
    [SerializeField] private Image _leftImage;
    [SerializeField] private Image _rightImage;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void ChangeBar(float currentValue, float maxValue)
    {
        base.ChangeBar(currentValue, maxValue);
        if (_slider.value == 1)
        {
            _rightImage.color = new Color32(255, 255, 255, 255);
        }
        else if (_slider.value != 1)
        {
            _rightImage.color = new Color32(155, 140, 140, 60);
        }

        if (_slider.value == 0)
        {
            _leftImage.color = new Color32(155, 140, 140, 60);
        }
        else if (_slider.value != 0)
        {
            _leftImage.color = new Color32(255, 255, 255, 255);
        }
    }
}
