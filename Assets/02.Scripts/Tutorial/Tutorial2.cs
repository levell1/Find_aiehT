using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    public ItemObject TutorialItem;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
        TutorialItem.OnInteractionNatureItem += InteractionItem;
    }

    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;
    }

    private void InteractionItem()
    {
        _tutorialManager.DoMove(_duration, _easeType);
    }
}
