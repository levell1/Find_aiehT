using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial6 : MonoBehaviour
{
    private RestaurantUI RestaurantUI;
    private TutorialManager _tutorialManager;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
    }
    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;
    }

    //private void FixedUpdate()
    //{
    //    if ()
    //    {
    //        _tutorialManager.DoMove(_duration, _easeType);
    //    }
    //}

}
