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
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }


    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {

        InputActions.Dispose();
        InputActions = new PlayerInputActions();

        if (scene.name == SceneName.TycoonScene)
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
