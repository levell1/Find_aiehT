using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Tutorials;
    private int _index;
    public Canvas Canvas;
    private TutorialGuideBook _tutorialGuideBook;
    public Image TutorialImage;
    public TextMeshProUGUI TutorialText;

    private float _waitTime;

    private Coroutine _coroutine;

    private void Awake()
    {
        _tutorialGuideBook = GetComponentInChildren<TutorialGuideBook>();
        if(GameManager.Instance.GlobalTimeManager.Day != 0)
        {
            Destroy(Canvas.gameObject);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _index = 0;
        Canvas.gameObject.SetActive(false);
        if (Tutorials.Length == 0) return;

        foreach (var tutorial in Tutorials)
        {
            tutorial.SetActive(false);
        }

        StartTutorial(); //시작시 딜레이를 조금 주고 싶다면 인보크 고민
    }

    private void LateUpdate()
    {
        if (!Tutorials[_index].activeSelf)
        {
            if (_index + 1 == Tutorials.Length) return;

            ++_index;
            StartTutorial();
        }
    }

    private void StartTutorial()
    {
        if (_index >= 1 && !_tutorialGuideBook.gameObject.activeSelf)
        {
            Canvas.gameObject.SetActive(true);
        }

        UPdateUI();
        Tutorials[_index].SetActive(true);
    }

    public void DoMove(float _duration, Ease _easeType)
    {
        if (_coroutine == null)
        {
            TutorialImage.DOFade(0f, _duration).SetEase(_easeType);
            TutorialText.DOFade(0f, _duration).SetEase(_easeType);
            _waitTime = _duration;
            _coroutine = StartCoroutine(EndTutorial());
        }
    }

    private void UPdateUI()
    {
        TutorialImage.color = new Color(1, 1, 1, 1);
        TutorialText.color = new Color(0, 0, 0, 1);
    }

    private IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(_waitTime);
        Tutorials[_index].SetActive(false);
        _coroutine = null;
    }

}
