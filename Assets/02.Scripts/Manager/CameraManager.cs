using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera MainCamera;
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachinePOV VirtualcameraPov;

    public float CamaraSpeed;

    private void Awake()
    {
        VirtualcameraPov= VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        
    }

    public void SaveCamSpeed()
    {
        CamaraSpeed = VirtualcameraPov.m_VerticalAxis.m_MaxSpeed;
    }

    public void DontMoveCam() 
    {
        VirtualcameraPov.m_VerticalAxis.m_MaxSpeed = 0;
        VirtualcameraPov.m_HorizontalAxis.m_MaxSpeed = 0;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.01f;
        
    }

    public void ReturnCamSpeed()
    {
        Time.timeScale = 1f;
        if (CamaraSpeed==0)
        {
            CamaraSpeed = GameManager.Instance.UIManager.CameraSpeed;
        }
        VirtualcameraPov.m_VerticalAxis.m_MaxSpeed = CamaraSpeed;
        VirtualcameraPov.m_HorizontalAxis.m_MaxSpeed = CamaraSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
