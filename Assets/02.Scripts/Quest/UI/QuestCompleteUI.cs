using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompleteUI : MonoBehaviour
{
    [SerializeField] private GameObject _completeObject;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private TMP_Text questText;
    [SerializeField] private Image _completeImage;

    public void ShowCompleteUI(string Description,string QuestText) 
    {
        gameObject.SetActive(true);
        questDescription.text = Description;
        questText.text = QuestText;
        StartCoroutine(CompleteUIFade());
    }

    private IEnumerator CompleteUIFade()
    {
        _completeObject.SetActive(true);
        Tween tween = _completeImage.DOFade(0.7f, 1f);
        questDescription.DOFade(1f, 1f);
        questText.DOFade(1f, 1f);
        yield return tween.WaitForCompletion();

        yield return new WaitForSeconds(2f);

        tween = _completeImage.DOFade(0.0f, 1f);
        questDescription.DOFade(0f, 1f);
        questText.DOFade(0f, 1f);
        yield return tween.WaitForCompletion();

        _completeObject.SetActive(false);
    }
}
