using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToVillage : MonoBehaviour
{
    public float ReturnDelay;
    public TextMeshProUGUI Returntext;
    public GameObject ReturnEffect;
    private float _curHealth;
    private bool _isRunning;

    private PlayerInput _playerInput;
    private HealthSystem _healthSystem;

    private Coroutine _coroutine;

    private void Awake()
    {
        _isRunning = false;
        Returntext.gameObject.SetActive(false);
        _playerInput = GetComponent<PlayerInput>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (_curHealth != _healthSystem.Health && _isRunning)
        {
            CancelReturn();
        }

        if (SceneManager.GetActiveScene().name == SceneName.HuntingScene)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                GoVillage();
            }
        }
    }

    private void GoVillage()
    {
        if (_coroutine != null)
        {
            CancelReturn();
            return;
        }

        _playerInput.InputActions.Disable();
        _curHealth =_healthSystem.Health;


        _coroutine = StartCoroutine(GoVillageCo());
    }

    private void CancelReturn()
    {
        _playerInput.InputActions.Enable();

        _curHealth = _healthSystem.Health;
        _isRunning = false;
        ReturnEffect.SetActive(false);
        Returntext.gameObject.SetActive(false);

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private IEnumerator GoVillageCo()
    {
        _isRunning = true;
        ReturnEffect.SetActive(true);
        Returntext.gameObject.SetActive(true);
        yield return new WaitForSeconds(ReturnDelay);
        ReturnEffect.SetActive(false);
        Returntext.gameObject.SetActive(false);
        GameManager.Instance.Player.transform.position = new Vector3(-3, 0, 10);
        LoadingSceneController.LoadScene(SceneName.VillageScene);
        _isRunning = false;
        _coroutine = null;
    }
}
