using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tutorial5 : MonoBehaviour
{
    private TutorialManager _tutorialManager;
    private Player _player;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;
    public string TutorialTxt;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
        _player = GameManager.Instance.Player.GetComponent<Player>();
    }

    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;
        _player.HealthSystem.TakeDamage(10);
    }

    private void FixedUpdate()
    {
        if (_player.HealthSystem.MaxHealth == _player.HealthSystem.Health)
        {
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }
}
