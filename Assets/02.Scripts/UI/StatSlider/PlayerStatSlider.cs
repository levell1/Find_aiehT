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

    protected override void ChangeBar(int _currentValue,int _maxValue)
    {
        base.ChangeBar(_currentValue, _maxValue);
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
