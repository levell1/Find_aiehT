using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseSlider : MonoBehaviour
{
    protected Slider _slider;
    [SerializeField] private TMP_Text _Text;

    [SerializeField] private int _currentValue;
    [SerializeField] private int _maxValue;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected virtual void ChangeBar(int _currentValue, int _maxValue)
    {
        _Text.text = (_currentValue + "/" + _maxValue);
        _slider.value = (float)_currentValue / (float)_maxValue;

    }
}
