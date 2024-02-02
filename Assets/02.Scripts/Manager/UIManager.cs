using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private int _canvasSortOrder = 5;
    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    public Dictionary<string, GameObject> PopupDic = new Dictionary<string, GameObject>();
    public float _cameraVSpeed;
    public float _cameraHSpeed;
    private CinemachinePOV _virtualcamera;
    

    public void CreateCanvas() 
    {
        _virtualcamera = GameManager.Instance.CameraManager.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
        GameObject uiObject = GameObject.Find("UIs");
        if (uiObject == null)
        {
            uiObject = new GameObject("UIs");
            Object.DontDestroyOnLoad(uiObject);
            var pre = Resources.LoadAll<GameObject>("UI/Canvas");
            foreach (var p in pre) 
            {
                PopupDic.Add(p.name, Object.Instantiate(p,uiObject.transform));
                PopupDic[p.name].SetActive(false);
            }
        }
    }
    
    public void ShowCanvas(string uiname)
    {
        if (!PopupDic[uiname].activeSelf) { 
            PopupDic[uiname].GetComponent<Canvas>().sortingOrder = _canvasSortOrder;
            _popupStack.Push(PopupDic[uiname]);
            PopupDic[uiname].SetActive(true);
            _canvasSortOrder++;
            Cursor.lockState = CursorLockMode.None;
            _cameraVSpeed = _virtualcamera.m_VerticalAxis.m_MaxSpeed;
            _cameraHSpeed = _virtualcamera.m_HorizontalAxis.m_MaxSpeed;
            _virtualcamera.m_VerticalAxis.m_MaxSpeed = 0;
            _virtualcamera.m_HorizontalAxis.m_MaxSpeed = 0;
        }
    }

    public void CloseLastCanvas()
    {
        if (_popupStack.Count == 1)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (_popupStack.Count == 0)
        {
            ShowCanvas(UIName.SettingUI);
        }
        else
        {
            GameObject currentUi = _popupStack.Pop();
            if (currentUi == PopupDic[UIName.RestaurantUI])
            {
                _popupStack.Push(currentUi);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                _virtualcamera.m_VerticalAxis.m_MaxSpeed = _cameraVSpeed;
                _virtualcamera.m_HorizontalAxis.m_MaxSpeed = _cameraHSpeed;
                currentUi.SetActive(false);
                currentUi = null;
                _canvasSortOrder--;
            }
        }
    }

    public void CloseAllCanvas()
    {
        for (int i = 0; i < _popupStack.Count; i++)
        {
            GameObject currentUi = _popupStack.Pop();
            currentUi.SetActive(false);
            currentUi = null;
            _canvasSortOrder--;
        }
        _virtualcamera.m_VerticalAxis.m_MaxSpeed = _cameraVSpeed;
        _virtualcamera.m_HorizontalAxis.m_MaxSpeed = _cameraHSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
