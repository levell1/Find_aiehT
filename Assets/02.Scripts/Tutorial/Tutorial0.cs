using DG.Tweening;
using UnityEngine;

public class Tutorial0 : MonoBehaviour
{
    private TutorialManager _tutorialManager;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;
    public string TutorialTxt2;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
    }

    private void OnEnable()
    {
        if (TutorialTxt2 != string.Empty)
        {
            _tutorialManager.TutorialText.text = TutorialTxt + "\n" + TutorialTxt2;
        }
        else
        {
            _tutorialManager.TutorialText.text = TutorialTxt;
        }
        Invoke("NextTutorial", _duration);
    }

    private void NextTutorial()
    {
        _tutorialManager.DoMove(_duration, _easeType);
    }
}
