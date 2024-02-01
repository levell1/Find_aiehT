using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : BaseUI
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
