using System.Collections.Generic;
using UnityEngine;

public class UIManager
{

    private int _canvasSortOrder = 5;
    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    public Dictionary<string, GameObject> _popupDic = new Dictionary<string, GameObject>();
    
    public void CreateCanvas() 
    {
        GameObject uiObject = GameObject.Find("Uis");
        if (uiObject == null)
        {
            uiObject = new GameObject("Uis");
            Object.DontDestroyOnLoad(uiObject);
            var pre = Resources.LoadAll<GameObject>("UI/Canvas");
            foreach (var p in pre) 
            {
                _popupDic.Add(p.name, Object.Instantiate(p,uiObject.transform));
                _popupDic[p.name].SetActive(false);
            }
        }
    }

    public void ShowCanvas(string uiname)
    {
        if (!_popupDic[uiname].activeSelf) { 
        _popupDic[uiname].GetComponent<Canvas>().sortingOrder = _canvasSortOrder;
        _popupStack.Push(_popupDic[uiname]);
        _popupDic[uiname].SetActive(true);
        _canvasSortOrder++;
        Cursor.lockState = CursorLockMode.None;
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
            ShowCanvas("SettingUI");
        }
        else 
        { 
            GameObject currentUi = _popupStack.Pop();
            currentUi.SetActive(false);
            currentUi = null;
            _canvasSortOrder--;
        }
    }


}
