using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public UIInputAction InputActions { get; private set; }

    private void Awake()
    {
        InputActions = new UIInputAction();
        InputActions.Enable();
        InputActions.UIActionMap.ShowMouseCursor.performed += OnShowMouseCursor;
    }

    private void OnShowMouseCursor(InputAction.CallbackContext context)
    {
        if(!Cursor.visible)
        {
            Cursor.visible = true;
            GameManager.Instance.CameraManager.SaveCamSpeed();
            GameManager.Instance.CameraManager.DontMoveCam();
        }

        else
        {
            Cursor.visible = false;
            GameManager.Instance.CameraManager.ReturnCamSpeed();
        }

    }
}
