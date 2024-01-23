using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
 
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }
    public PlayerInputActions.TycoonPlayerActions TycoonPlayerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        //PlayerActions = InputActions.Player;

        InIt();
    }

    private void InIt()
    {
       
        if(SceneManager.GetActiveScene().name == "MWJ")
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
