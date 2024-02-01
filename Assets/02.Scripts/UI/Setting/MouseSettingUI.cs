using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MouseSettingUI : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    private void Start()
    {
        _slider.onValueChanged.AddListener(SetCam);
    }
    void SetCam(float value)
    {
        GameManager.Instance.UIManager._cameraHSpeed = value;
        GameManager.Instance.UIManager._cameraVSpeed = value;
    }
}