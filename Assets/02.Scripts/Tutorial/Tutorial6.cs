using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial6 : MonoBehaviour
{
    private Inventory _inventory;
    public Image Image;
    private TutorialManager _tutorialManager;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;
    public string TutorialTxt2;

    private void Awake()
    {
        _inventory = GameManager.Instance.Inventory.GetComponent<Inventory>();
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

    private void FixedUpdate()
    {
        if (_inventory.Slots.Count <=   0)
        {
            Image.gameObject.SetActive(false);
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }

}
