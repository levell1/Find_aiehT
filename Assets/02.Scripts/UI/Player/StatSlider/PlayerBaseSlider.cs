using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerBaseSlider : MonoBehaviour
{
    protected Slider _slider;
    [SerializeField] private TMP_Text _Text;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected virtual void ChangeBar(float _currentValue, float _maxValue)
    {
        _Text.text = (_currentValue + "/" + _maxValue);
        _slider.value =_currentValue / _maxValue;
    }
}
