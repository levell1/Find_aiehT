using DG.Tweening;
using UnityEngine;

public class Tutorial4 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    public EnemyHealthSystem[] TutorialChick;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;
    public string TutorialTxt2;

    private int EnemyCount;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
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
