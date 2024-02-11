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
        _fadeImage = GetComponentInChildren<Image>();
    }
    void Start()
    {
        var dir = transform.forward * 5f;
        NextRoomPosition = transform.position + dir;
    }

    public IEnumerator GoNextRoom()
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        _fadeImage.DOFade(0f, 1f);
        _fadeImage.gameObject.SetActive(false);
    }
}
