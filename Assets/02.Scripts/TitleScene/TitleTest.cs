using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTest : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _globalTimeManager;
    [SerializeField] private GameObject _mainCam;

    private void Awake()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name!=SceneName.TitleScene)
        {
            _globalTimeManager.SetActive(true);
            _mainCam.SetActive(true);
            _player.SetActive(true);
            _playerUI.SetActive(true);

        }
        if (scene.name == SceneName.TycoonScene)
        {
            _globalTimeManager.SetActive(false);
            _playerUI.SetActive(false);

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == SceneName.TitleScene || GameManager.Instance.GlobalTimeManager.Day == 0)
            {
                return;
            }
            if (GameManager.Instance.UIManager.PopupStack .Count!=0)
            {
                if (GameManager.Instance.UIManager.PopupStack.Peek().name == UIName.InventoryUI)
                {
                    GameManager.Instance.UIManager.CloseAllCanvas();
                    return;
                }
            }
            GameManager.Instance.UIManager.CloseLastCanvas();
        }
    }
}
