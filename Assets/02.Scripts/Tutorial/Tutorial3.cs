using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;
    [SerializeField] private Vector3 paramVector;

    public Image TutorialImage;
    public TextMeshProUGUI TutorialText;
    public GameObject Fence;

    private Coroutine _coroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DoMove();
        }
    }

    private void DoMove()
    {
        if (_coroutine == null)
        {
            TutorialImage.DOFade(0f, _duration).SetEase(_easeType);
            TutorialText.DOFade(0f, _duration).SetEase(_easeType);
            Fence.transform.DOMoveX(paramVector.x, _duration);
            _coroutine = StartCoroutine(EndTutorial());
        }
    }

    private IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
        _coroutine = null;
    }
}
