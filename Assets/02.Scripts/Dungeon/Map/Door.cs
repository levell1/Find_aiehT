using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Vector3 NextRoomPosition;
    [SerializeField] private Image _fadeImage;
    [SerializeField] private EnemyHealthSystem[] enemyHealthSystems; 

    private BoxCollider _boxCollider;
    private void Awake()
    {
        _boxCollider =GetComponent<BoxCollider>();
    }
    void Start()
    {
        var dir = transform.forward * 10f;
        NextRoomPosition = transform.position + dir;
        _boxCollider.enabled = false;
    }
    private void Update()
    {
        if (_boxCollider.enabled == false)
        {
            foreach (var enemy in enemyHealthSystems)
            {
                if (enemy.IsDead == false)
                {
                    break ;
                }
                _boxCollider.enabled = true;
            }
        }        
    }

    public void FadeImage() 
    {
        StartCoroutine(GoNextRoomFade());
    }
    private IEnumerator GoNextRoomFade()
    {
        _fadeImage.gameObject.SetActive(true);
        Tween tween = _fadeImage.DOFade(1.0f, 2f);

        yield return tween.WaitForCompletion();

        tween = _fadeImage.DOFade(0.0f, 2f);
        yield return tween.WaitForCompletion();

        _fadeImage.gameObject.SetActive(false);
    }
}
