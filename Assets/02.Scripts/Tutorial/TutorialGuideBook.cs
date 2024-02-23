using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuideBook : MonoBehaviour
{
    [SerializeField] private Button _prevBtn;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _exitBtn;

    [SerializeField] private TextMeshProUGUI _pageCount;

    [SerializeField] private Image[] _tutorialImages;

    private int _index;

    private void Start()
    {
        _prevBtn.onClick.AddListener(PreviewImage);
        _nextBtn.onClick.AddListener(NextImage);
        _exitBtn.onClick.AddListener(ExitButton);
    }

    private void OnEnable()
    {
        GameManager.Instance.CameraManager.DontMoveCam();

        _index = 0;

        _prevBtn.interactable = false;
        _nextBtn.interactable = true;
        _exitBtn.interactable = false;

        foreach (var images in _tutorialImages)
        {
            images.gameObject.SetActive(false);
        }
        _tutorialImages[_index].gameObject.SetActive(true);

        UpdateUI();
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        { 
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.CameraManager.DisableCam();
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.CameraManager.ReturnCamSpeed();
    }

    private void PreviewImage()
    {
        _tutorialImages[_index].gameObject.SetActive(false);
        --_index;
        _tutorialImages[_index].gameObject.SetActive(true);
        _nextBtn.interactable = true;
        if (_index == 0)
        {
            _prevBtn.interactable = false;
        }

        UpdateUI();
    }

    private void NextImage()
    {
        _tutorialImages[_index].gameObject.SetActive(false);
        ++_index;
        _tutorialImages[_index].gameObject.SetActive(true);
        _prevBtn.interactable = true;
        if (_index + 1 == _tutorialImages.Length)
        {
            _nextBtn.interactable = false;
            _exitBtn.interactable = true;
        }

        UpdateUI();
    }

    private void ExitButton()
    {
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        _pageCount.text = string.Format($"{_index + 1} / {_tutorialImages.Length}");
    }

}
