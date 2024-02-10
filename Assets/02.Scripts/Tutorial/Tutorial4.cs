using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial4 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    public EnemyHealthSystem[] TutorialChick;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;

    private int EnemyCount;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
    }

    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;

        EnemyCount = TutorialChick.Length;
        for (int i = 0; i < TutorialChick.Length; ++i)
        {
            TutorialChick[i].gameObject.SetActive(true);
            TutorialChick[i].OnDie += KillEnemy;
        }
    }

    private void KillEnemy()
    {
        --EnemyCount;
        if(EnemyCount == 0)
        {
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }
}
