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
            //_globalTimeManager.SetActive(true);
            _mainCam.SetActive(true);
            _player.SetActive(true);
            _playerUI.SetActive(true);

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
            _globalTimeManager.SetActive(false);
            _playerUI.SetActive(false);

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == SceneName.TutorialScene && GameManager.Instance.GlobalTimeManager.Day == 0 || SceneManager.GetActiveScene().name == SceneName.TycoonScene && GameManager.Instance.GlobalTimeManager.Day == 0)
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-45f, 1f, -80f);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.Player.transform.position = new Vector3(5f, 1f, 120f);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.Player.transform.position = new Vector3(80f, 1f, -40f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.Instance.Player.transform.position = new Vector3(-90f, 1f, 50f);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameManager.Instance.Player.transform.position = new Vector3(120f, 1f, 0f);
        }
    }
}
