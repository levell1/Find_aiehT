using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial0 : MonoBehaviour
{
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
        Invoke("NextTutorial", _duration);
    }

    private void NextTutorial()
    {
        _tutorialManager.DoMove(_duration, _easeType);
    }



}
