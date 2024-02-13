using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Vector3 NextRoomPosition;
    private Image _fadeImage;

    private void Awake()
    {
        _fadeImage = FindObjectOfType<Image>();
    }
    void Start()
    {
        var dir = transform.forward * 10f;
        NextRoomPosition = transform.position + dir;
    }

    public void FadeImage() 
    {
        StartCoroutine(GoNextRoomFade());
    }
    private IEnumerator GoNextRoomFade()
    {
        _fadeImage.gameObject.SetActive(true);
        Tween tween = _fadeImage.DOFade(1.0f, 2f);

        yield return tween.WaitForCompletion();

        tween = _fadeImage.DOFade(0.0f, 2f);
        yield return tween.WaitForCompletion();

        _fadeImage.gameObject.SetActive(false);
    }
}
