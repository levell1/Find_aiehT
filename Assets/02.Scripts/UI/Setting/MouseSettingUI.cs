using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MouseSettingUI : MonoBehaviour
{
    private Slider _slider;
    private CinemachinePOV _virtualcamera;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _virtualcamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
    }
    private void Start()
    {
        _slider.onValueChanged.AddListener(SetCam);
    }
    void SetCam(float value)
    {
        _virtualcamera.m_VerticalAxis.m_MaxSpeed = value;
        _virtualcamera.m_HorizontalAxis.m_MaxSpeed = value;
    }
}
