using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Tutorial1 : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Ease easeType;

    public Image Image;

    private void Awake()
    {
        DoMove();
    }


    private void DoMove()
    {
        Image.DOFade(0f, duration).SetEase(easeType);
    }
}
