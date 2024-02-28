
using DG.Tweening;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private TMP_Text _endingText;
    private int count = 20;


    private void Start()
    {
        StartCoroutine(FadeEndingImage());
    }

    private IEnumerator FadeEndingImage()
    {
        _fadeImage.gameObject.SetActive(true);
        Tween tween = _fadeImage.DOFade(1.0f, 2f);
        yield return tween.WaitForCompletion();
        while (count >= 0)
        {
            _endingText.text = count.ToString();
            count--;
            yield return new WaitForSecondsRealtime(1f);
        }

        GameManager.Instance.GlobalTimeManager.GoodMorning();
    }
}
