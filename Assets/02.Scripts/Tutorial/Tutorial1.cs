using UnityEngine;
using DG.Tweening;

public class Tutorial1 : MonoBehaviour
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player))
        {
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }
}
