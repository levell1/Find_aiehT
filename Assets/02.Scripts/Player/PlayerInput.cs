using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
 
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
    public PlayerInputActions.TycoonPlayerActions TycoonPlayerActions { get; private set; }

    private InputActionAsset inputActionAsset;
    private InputActionMap _playerInputActionMap;
    private InputActionMap _TycoonPlayerInputActionMap;

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        //PlayerActions = InputActions.Player;
        SceneManager.sceneLoaded += LoadedsceneEvent;
        //InIt();
    }

    private void LoadedsceneEvent(Scene scece, LoadSceneMode mode)
    {
        //inputActionAsset = InputActions.asset;

        //_playerInputActionMap = inputActionAsset.FindActionMap("Player");
        //_TycoonPlayerInputActionMap = inputActionAsset.FindActionMap("TycoonPlayer");

        if (scece.name == "LodingScene")
        {
            return;
        }
        else if (scece.name == "TycoonScene")
        {
            //InputSystem.DisableAllEnabledActions();

            TycoonPlayerActions = InputActions.TycoonPlayer;
            
            //_playerInputActionMap.Disable();
            //_TycoonPlayerInputActionMap.Enable();
        }
        else
        {
            //InputSystem.DisableAllEnabledActions();

            PlayerActions = InputActions.Player;

            //_playerInputActionMap.Enable();
            //_TycoonPlayerInputActionMap.Disable();
        }
    }

    private void InIt()
    {
       
        if(SceneManager.GetActiveScene().name == "TycoonScene")
        {
            TycoonPlayerActions = InputActions.TycoonPlayer;
        }
        else
        {
            PlayerActions = InputActions.Player;
        }

    }


    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

}
