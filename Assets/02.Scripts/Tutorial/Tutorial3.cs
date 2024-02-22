using UnityEngine;
using DG.Tweening;

public class Tutorial3 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;
    [SerializeField] private Vector3 _paramVector;
    public string TutorialTxt;
    public string TutorialTxt2;
    public GameObject Fence;

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

    private void Update()
    {
        if (GameManager.Instance.UIManager.PopupDic[UIName.InventoryUI].activeSelf)
        {
            _tutorialManager.DoMove(_duration, _easeType);
            Fence.transform.DOMoveX(_paramVector.x, _duration);
        }
    }
}
