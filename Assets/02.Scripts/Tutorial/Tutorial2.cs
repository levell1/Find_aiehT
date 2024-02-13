using DG.Tweening;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    public ItemObject TutorialItem;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;
    public string TutorialTxt2;
    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
        TutorialItem.OnInteractionNatureItem += InteractionItem;
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

    private void InteractionItem()
    {
        _tutorialManager.DoMove(_duration, _easeType);
    }
}
