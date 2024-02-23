using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceTutorialUI : MonoBehaviour
{
    private GameObject _rpgGuideBook;
    private GameObject _tycoonGuideBook;
    [SerializeField] private Button _rpgBtn;
    [SerializeField] private Button _tycoonBtn;

    private void Start()
    {
        _rpgGuideBook = GameManager.Instance.UIManager.PopupDic[UIName.TutorialGuideBook1];
        _tycoonGuideBook = GameManager.Instance.UIManager.PopupDic[UIName.TutorialGuideBook2];

        _rpgBtn.onClick.AddListener(RPGTutorial);
        _tycoonBtn.onClick.AddListener(TycoonTutorial);
    }

    private void RPGTutorial()
    {
        _tycoonGuideBook.SetActive(false);
        GameManager.Instance.UIManager.CloseAllCanvas();
        _rpgGuideBook.SetActive(true);
    }

    private void TycoonTutorial()
    {
        _rpgGuideBook.SetActive(false);
        GameManager.Instance.UIManager.CloseAllCanvas();
        _tycoonGuideBook.SetActive(true);
    }
}
