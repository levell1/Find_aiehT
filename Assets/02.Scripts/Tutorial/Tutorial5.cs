using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tutorial5 : MonoBehaviour
{
    private Player _player;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public Image TutorialImage;
    public TextMeshProUGUI TutorialText;

    private Coroutine _coroutine;

    private void Awake()
    {
        _player = GameManager.Instance.Player.GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HealthSystem.TakeDamage(100);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _player.HealthSystem.Healing(100);
            DoMove();
        }
    }


    private void DoMove()
    {
        if (_coroutine == null)
        {
            TutorialImage.DOFade(0f, _duration).SetEase(_easeType);
            TutorialText.DOFade(0f, _duration).SetEase(_easeType);
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
