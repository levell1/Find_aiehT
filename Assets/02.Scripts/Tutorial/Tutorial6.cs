using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Tutorial6 : MonoBehaviour
{
    private Inventory _inventory;
    public Image Image;
    private TutorialManager _tutorialManager;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easeType;

    public string TutorialTxt;

    private void Awake()
    {
        _inventory = GameManager.Instance.Inventory.GetComponent<Inventory>();
        _tutorialManager = GetComponentInParent<TutorialManager>();
    }
    private void OnEnable()
    {
        _tutorialManager.TutorialText.text = TutorialTxt;
    }

    private void FixedUpdate()
    {
        if (_inventory.Slots.Count <=   0)
        {
            Image.gameObject.SetActive(false);
            _tutorialManager.DoMove(_duration, _easeType);
        }
    }

}
