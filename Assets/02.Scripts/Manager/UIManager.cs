using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private int _canvasSortOrder = 5;
    public Stack<GameObject> PopupStack = new Stack<GameObject>();
    public Dictionary<string, GameObject> PopupDic = new Dictionary<string, GameObject>();
    public float CameraSpeed;
    

    public void CreateCanvas() 
    {
        CameraSpeed = 0.5f;
        GameObject uiobject = GameObject.Find("Dont");
        PopupDic.Add(UIName.InventoryUI, uiobject.transform.Find(UIName.InventoryUI).gameObject);
        PopupDic.Add(UIName.ShopUI, uiobject.transform.Find(UIName.ShopUI).gameObject);
        PopupDic.Add(UIName.PlayerBaseUI, uiobject.transform.Find(UIName.PlayerBaseUI).gameObject);
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
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.UISound,Vector3.zero,1f);
        if (!PopupDic[uiname].activeSelf) { 
            PopupDic[uiname].GetComponent<Canvas>().sortingOrder = _canvasSortOrder;
            PopupStack.Push(PopupDic[uiname]);
            PopupDic[uiname].SetActive(true);
            _canvasSortOrder++;
            GameManager.Instance.CameraManager.DontMoveCam();
        }
    }

    public void CloseLastCanvas()
    {
        if (PopupStack.Count == 0)
        {
            ShowCanvas(UIName.SettingUI);
        }
        else
        {
            GameObject currentUi = PopupStack.Pop();
            /*if (currentUi == PopupDic[UIName.RestaurantUI])
            {
                PopupStack.Push(currentUi);
                Cursor.lockState = CursorLockMode.None;
                return;
            }*/
            if (currentUi == PopupDic[UIName.SettingUI])
            {
                GameManager.Instance.CameraManager.CameraSpeed = CameraSpeed;
            }
            if (PopupStack.Count == 0)
            {
                GameManager.Instance.CameraManager.ReturnCamSpeed();
            }
            currentUi.SetActive(false);
            currentUi = null;
            _canvasSortOrder--;
        }
    }

    public void CloseAllCanvas()
    {
        int a = PopupStack.Count;
        for (int i = 0; i < a; i++)
        {
            GameObject currentUi = PopupStack.Pop();
            currentUi.SetActive(false);
            currentUi = null;
            _canvasSortOrder--;
        }
        GameManager.Instance.CameraManager.ReturnCamSpeed();
    }
}
