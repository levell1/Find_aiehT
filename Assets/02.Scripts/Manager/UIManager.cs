
using System.Collections.Generic;
using UnityEngine;


public class UIManager
{

    private int _canvasSortOrder = 5;

    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    public Dictionary<string, GameObject> _popupDic = new Dictionary<string, GameObject>();


    public void CreateCanvas() 
    {
        var pre = Resources.LoadAll<GameObject>("UI/Canvas");
        foreach (var p in pre) 
        {
            Debug.Log(p.name);
            _popupDic.Add(p.name, Object.Instantiate(p));
            _popupDic[p.name].SetActive(false);
        }
    }

    public void Popup(string uiname)
    {
        _popupDic[uiname].GetComponent<Canvas>().sortingOrder = _canvasSortOrder;
        _popupStack.Push(_popupDic[uiname]);
        _popupDic[uiname].SetActive(true);
        _canvasSortOrder++;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseLastPopup()
    {
        if (_popupStack.Count == 0) 
        {
            Popup("SettingUI");
        }
        else { 
        GameObject currentUi = _popupStack.Pop();
        currentUi.SetActive(false);
        currentUi = null;
        _canvasSortOrder--;
        }
        
    }

}
