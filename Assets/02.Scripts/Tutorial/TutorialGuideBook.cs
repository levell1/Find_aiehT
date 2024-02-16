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

    private int _index = 0;

    private void Start()
    {
        foreach (var images in _tutorialImages)
        {
            images.gameObject.SetActive(false);
        }
        _tutorialImages[_index].gameObject.SetActive(true);

        _prevBtn.onClick.AddListener(PreviewImage);
        _nextBtn.onClick.AddListener(NextImage);
        _exitBtn.onClick.AddListener(ExitButton);

        _prevBtn.interactable = false;
        _exitBtn.interactable = false;

        UpdateUI();
    }

    private void OnEnable()
    {
        //Invoke("CursorTimeLock", 1f);

        GameManager.Instance.CameraManager.SaveCamSpeed();
        GameManager.Instance.CameraManager.DontMoveCam();
    }

    //private void OnDisable()
    //{
    //    CancelInvoke("CursorTimeLock");
    //}

    private void CursorTimeLock()
    {
        //Time.timeScale = 0.01f;
        //Cursor.lockState = CursorLockMode.None;

        GameManager.Instance.CameraManager.SaveCamSpeed();
        GameManager.Instance.CameraManager.DontMoveCam();
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
        //Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked;

        GameManager.Instance.CameraManager.ReturnCamSpeed();

        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        _pageCount.text = string.Format($"{_index + 1} / {_tutorialImages.Length}");
    }

}
