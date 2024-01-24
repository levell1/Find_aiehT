using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWJTest : MonoBehaviour
{
    [SerializeField] private GameObject _prepareUI;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PrepareUIEnable(_prepareUI.activeSelf);
        }
    }
    
    private void PrepareUIEnable(bool isActive)
    {
        _prepareUI.SetActive(!isActive);

        Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
        
    }
}
