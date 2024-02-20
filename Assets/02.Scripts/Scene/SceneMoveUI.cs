using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneMoveUI : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private GlobalTimeManager _globalTimeManager;
    [SerializeField] private Image _backImage;
    public TextMeshProUGUI Description;
    public string CurrentSceneName;
    private float _duration = 2f;

    private void Awake()
    {
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;

        _backImage.color = new Color(0, 0, 0, 0);
        Description.color = new Color(1, 1, 1, 0);

        ActiveUI();
    }

    private void ActiveUI()
    {
        _backImage.DOFade(1f, _duration);
        Description.DOFade(1f, _duration);
        StartCoroutine(MoveScene());
    }

    private IEnumerator MoveScene()
    {
        yield return new WaitForSeconds(_duration);
        
        if (CurrentSceneName == SceneName.VillageScene && _globalTimeManager.EventCount == 0)
        {
            GameManager.Instance.Player.transform.position = new Vector3(-4, 0, 19);
        }
        else if (CurrentSceneName == SceneName.VillageScene && _globalTimeManager.EventCount == 1)
        {
            _healthSystem.Respawn();
            _globalTimeManager.DayTime = _globalTimeManager.NextMorning;
            GameManager.Instance.Player.transform.position = new Vector3(-15, 0, -160);
        }

        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.CameraManager.EnableCam();

        LoadingSceneController.LoadScene(CurrentSceneName);
        gameObject.SetActive(false);
    }

    
}
