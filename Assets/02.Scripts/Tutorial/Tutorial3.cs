using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Tutorial3 : MonoBehaviour
{
    private TutorialManager _tutorialManager;

    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;
    [SerializeField] private Vector3 _paramVector;
    public string TutorialTxt;
    public GameObject Fence;

    private void Awake()
    {
        _tutorialManager = GetComponentInParent<TutorialManager>();
    }
    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.UIManager.PopupDic[UIName.InventoryUI].activeSelf)
        {
            _tutorialManager.DoMove(_duration, _easeType);
            Fence.transform.DOMoveX(_paramVector.x, _duration);
        }
    }
}
