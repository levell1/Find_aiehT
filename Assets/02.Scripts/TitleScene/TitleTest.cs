using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTest : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerUI;
    [SerializeField] private GameObject _globalTimeManager;
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _timeCycle;

    private void Awake()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name!=SceneName.TitleScene)
        {
            _mainCam.SetActive(true);
            _player.SetActive(true);
            _playerUI.SetActive(true);
            _timeCycle.SetActive(true);

        }
        else if (scene.name == SceneName.TitleScene)
        {
            _globalTimeManager.SetActive(false);
            _mainCam.SetActive(false);
            _player.SetActive(false);
            _playerUI.SetActive(false);
        }

        if (scene.name == SceneName.TycoonScene)
        {
            _timeCycle.SetActive(false);
            _playerUI.SetActive(false);
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        
        if (Time.timeScale == 0&& Cursor.lockState == CursorLockMode.None||SceneManager.GetActiveScene().name==SceneName.TitleScene)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.ClickSound, Vector3.zero, 0.5f);
            }
        }
    }
}
