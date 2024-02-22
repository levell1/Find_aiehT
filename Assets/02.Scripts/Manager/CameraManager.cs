using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera;
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachinePOV VirtualcameraPov;
    public float CameraSpeed;

    private PlayerInput _playerInput; 
    private CinemachineVirtualCamera _tycoonCamera;

    private void Awake()
    { 
        VirtualcameraPov = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        _playerInput = GameManager.Instance.Player.GetComponent<PlayerInput>(); 
    }

    private void Start()
    {
        CameraSpeed = 0.5f;
    }

    public void DontMoveCam() 
    {
        Invoke("CursorTimeLock", 0.2f);
        DisableCam();
    }

    public void ReturnCamSpeed()
    {
        CancelInvoke("CursorTimeLock"); 

        Time.timeScale = 1f;

        VirtualcameraPov.m_VerticalAxis.m_MaxSpeed = CameraSpeed;
        VirtualcameraPov.m_HorizontalAxis.m_MaxSpeed = CameraSpeed;

        Cursor.lockState = CursorLockMode.Locked;

        EnableCam();
    }

    public void TycoonCamSetting()
    {
        _tycoonCamera = TycoonManager.Instance.TycoonVirtualCamera;
        _tycoonCamera.gameObject.SetActive(true);
        _tycoonCamera.Follow = VirtualCamera.Follow;
        _tycoonCamera.LookAt = VirtualCamera.LookAt;
    }

    public void NonTycoonCamSetting()
    {
        _tycoonCamera.gameObject.SetActive(false);
    }

    private void CursorTimeLock()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableCam()
    {
        if (_playerInput.gameObject.activeSelf)
        {
            _playerInput.InputActions.Disable(); 
            VirtualCamera.enabled = false;
        }

    }

    public void EnableCam()
    {
        if (_playerInput.gameObject.activeSelf&& VirtualCamera.enabled == false)
        {
            _playerInput.InputActions.Enable(); 
            VirtualCamera.enabled = true; 
        }
    }
}
