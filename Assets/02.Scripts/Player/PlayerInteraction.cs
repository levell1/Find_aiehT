using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public TMP_Text interactionText; // UI Text 요소를 가리키는 변수

    public LayerMask LayerMask;

    private Collider _interactCollider;
    [SerializeField] private Collider _playerCollider;

    private List<string> _interactionLayerList = new List<string>();

    private void Start()
    {
        InitializeCollider();
        _interactCollider =  GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == _playerCollider) { return; }

        if(other == _interactCollider) { return; }
    
        if(((1 << other.gameObject.layer) & LayerMask.value) != 0)
        {
            ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

            //TODO 아이템의 정보를 가져옴
            _interactionLayerList.Add(itemObject.ItemData.ObjName);
            UpdateUI();
            Test();
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

        if(itemObject != null)
        {
            _interactionLayerList.Remove(itemObject.ItemData.ObjName);
            UpdateUI();
        }
    }

    private void InitializeCollider()
    {
        _interactionLayerList.Clear();
        UpdateUI();
    }

    private void Test()
    {
        foreach(var item in _interactionLayerList)
        {
            Debug.Log(item);
        }
    }

    private void UpdateUI()
    {
        if (interactionText != null)
        {
            // UI Text 요소가 존재하면 리스트의 내용을 텍스트로 설정
            interactionText.text = "";

            foreach (var item in _interactionLayerList)
            {
                interactionText.text += "- " + item + "\n";
            }
        }
    }

}
