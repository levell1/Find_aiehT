using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialGuide : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public Image[] TutorialImage;
    public Image[] TutorialImageBack;
    public TextMeshProUGUI[] TutorialText;

    public Image Description;

    public float WaitTime;

    private int _index;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        GameManager.Instance.CameraManager.DisableCam();
    }

    private void OnDisable()
    {
        GameManager.Instance.CameraManager.EnableCam();
    }

    private void Start()
    {
        _index = 0;

        if (TutorialImage.Length == 0) return;

        foreach (var tutorial in TutorialImage)
        {
            tutorial.gameObject.SetActive(false);
        }

        Invoke("OffDescription", WaitTime);
    }

    private void LateUpdate()
    {
        if (_index == TutorialImage.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!TutorialImage[_index].gameObject.activeSelf)
        {
            Invoke("StartTutorial", WaitTime);
        }
    }

    private void StartTutorial()
    {
        TutorialImage[_index].gameObject.SetActive(true);
        OnDoMove();
    }

    private void OffDescription()
    {
        Description.gameObject.SetActive(false);
    }

    private void OnDoMove()
    {
        if (_coroutine == null)
        {
            TutorialImage[_index].DOFade(1f, _duration).SetEase(_easeType);
            TutorialImageBack[_index].DOFade(1f, _duration).SetEase(_easeType);
            TutorialText[_index].DOFade(1f, _duration).SetEase(_easeType);
            Invoke("OffDoMove", _duration + WaitTime);
        }
    }


    private void OffDoMove()
    {
        if (_coroutine == null)
        {
            TutorialImage[_index].DOFade(0f, _duration).SetEase(_easeType);
            TutorialImageBack[_index].DOFade(0f, _duration).SetEase(_easeType);
            TutorialText[_index].DOFade(0f, _duration).SetEase(_easeType);
            _coroutine = StartCoroutine(NextTutorial());
        }
    }

    private IEnumerator NextTutorial()
    {
        yield return new WaitForSeconds(_duration-1f);
        TutorialImage[_index].gameObject.SetActive(false);
        ++_index;
        _coroutine = null;
    }

}
