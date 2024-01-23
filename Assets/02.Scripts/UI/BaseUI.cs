using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public void CloseUI()
    {
        GameManager.instance.UIManager.CloseLastCanvas();
    }
    // 공통 기능 추가 생각

    private void OnEnable()
    {
        
    }
}
