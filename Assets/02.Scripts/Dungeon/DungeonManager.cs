using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    [SerializeField] private GameObject[] Stages;

    [SerializeField] private GameObject[] _monsterPrefabs;
    [SerializeField] private GameObject[] _nextStagePortal;
    [SerializeField] private GameObject _bossHpbar;
    private int _stagenum = 1;

    private void Awake()
    {
        _stagenum = 1;
    }
    private void Start()
    {
        Stages[0].SetActive(true);
        
        for (int i = 1; i < Stages.Length; i++)
        {
            Stages[i].SetActive(false);
        }
        NextStagePortalCheck();
    }
    public void GoNextRoom(Vector3 nextRoomPosition) 
    {
        StartCoroutine(GoNextRoomFade(nextRoomPosition));
    }

    public void GoNextStage() 
    {
        StartCoroutine(GoNextStageFade());
    }

    public void NextStagePortalCheck() 
    {
        if (_stagenum!=3) 
        { 
            GameObject portal = _nextStagePortal[Random.Range(_stagenum * 3 - 3, _stagenum * 3)];
            portal.SetActive(true);
            if (_stagenum == 2)
            {
                ParticleSystem[] particles = portal.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    var mains = particles[i].main;
                    mains.startColor = new Color(0.6f, 1f, 0.6f);
                }
            }
        }
        else
        {
            _bossHpbar.SetActive(true);
        }
    }

    public GameObject MonsterRandomSpawn(Vector3 pos,Transform transform)
    {
        int random = Random.Range(_stagenum*6-6, _monsterPrefabs.Length/2 * _stagenum);
        return Instantiate(_monsterPrefabs[random], pos,Quaternion.identity, transform);
    }
    public IEnumerator GoNextRoomFade(Vector3 nextRoomPos)
    {
        _fadeImage.gameObject.SetActive(true);
        Tween tween = _fadeImage.DOFade(1.0f, 2f);

        yield return tween.WaitForCompletion();
        GameManager.Instance.Player.transform.position = nextRoomPos;
        tween = _fadeImage.DOFade(0.0f, 2f);
        yield return tween.WaitForCompletion();

        _fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator GoNextStageFade()
    {
        _fadeImage.gameObject.SetActive(true);
        Tween tween = _fadeImage.DOFade(1.0f, 2f);
        yield return tween.WaitForCompletion();
        Stages[_stagenum - 1].SetActive(false);
        _stagenum++;
        Stages[_stagenum-1].SetActive(true);
        

        GameManager.Instance.Player.transform.position = Vector3.up;

        tween = _fadeImage.DOFade(0.0f, 2f);
        yield return tween.WaitForCompletion();
        
        _fadeImage.gameObject.SetActive(true);

        
        NextStagePortalCheck();
    }
}
