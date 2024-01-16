using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using UnityEngine;


public class UIManager
{
    private int _order = 10;

    private Stack<GameObject> _popupStack = new Stack<GameObject>();
    
    private Dictionary<string, GameObject> _popupUi = new Dictionary<string, GameObject>();

    private void Start() 
    {
        popup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastPopup();
        }
    }

    private void popup()
    {
        var pre = Resources.LoadAll<GameObject>("Canvas");
        foreach (var p in pre)
        {
            Debug.Log(p.name +","+p);
            _popupUi.Add(p.name, p);
        }
    }

    //스택 0 이면 설정창 +스택 ++
    private void CloseLastPopup()
    {
        if (_popupStack.Count == 0) 
        {
            
        }
        GameObject popup = _popupStack.Pop();
        popup.SetActive(false);
        popup = null;
        _order--; 
    }

}
