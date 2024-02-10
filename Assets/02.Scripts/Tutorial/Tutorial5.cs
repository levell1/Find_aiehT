using DG.Tweening;
using UnityEngine;


public class Tutorial5 : MonoBehaviour
{
    private TutorialManager _tutorialManager;
    private Player _player;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;
    public string TutorialTxt;
    public string TutorialTxt2;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
        _player = GameManager.Instance.Player.GetComponent<Player>();
    }

    private void OnEnable()
    {
        if (TutorialTxt2 != string.Empty)
        {
            _tutorialManager.TutorialText.text = TutorialTxt + "\n" + TutorialTxt2;
        }
        else
        {
            _tutorialManager.TutorialText.text = TutorialTxt;
        }
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
