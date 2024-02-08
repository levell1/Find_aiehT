using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Tutorial1 : MonoBehaviour
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }
}
