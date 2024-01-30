using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using static MoveSceneController;

public enum Enum
{
    ITEM,
    NPC
}

public class PlayerInteraction : MonoBehaviour
{
    public TMP_Text interactionText; // UI Text 요소를 가리키는 변수

    public LayerMask LayerMask;

    private Collider _interactCollider;
    [SerializeField] private Collider _playerCollider;

    private List<string> _interactionLayerList = new List<string>();

    private List<ItemObject> _interactItemObejctList = new List<ItemObject>();

    private Dictionary<int, Enum> LayerDic = new Dictionary<int, Enum>();

    private MoveSceneController _moveSceneController;
    private string _nextScene = string.Empty;
    public string NextSceneInfo;

    private void Start()
    {
        InitializeCollider();

        _interactCollider = GetComponent<Collider>();

        // 아이템
        LayerDic.Add(LayerMask.NameToLayer("Item"), Enum.ITEM);
        // 택시, 상점등
        LayerDic.Add(LayerMask.NameToLayer("NpcInteract"), Enum.NPC);

    }
    // TODO NPC 상호작용 추가
    private void OnTriggerEnter(Collider other)
    {
        if (other == _playerCollider) { return; }

        if (other == _interactCollider) { return; }

        /// DropItem의 경우
        if (LayerDic.TryGetValue(other.gameObject.layer, out Enum _objectType))
        {
            if (_objectType == Enum.ITEM)
            {
                ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

                _interactItemObejctList.Add(itemObject);

                _interactionLayerList.Add(itemObject.ItemData.ObjName);
                UpdateUI();
            }
            else if (_objectType == Enum.NPC)
            {
                if (other.gameObject.CompareTag("RealDoor")) //씬이동 (마을,사냥터,타이쿤,던전)
                {
                    _moveSceneController = other.gameObject.GetComponent<MoveSceneController>();
                    _nextScene = _moveSceneController.NextScene;
                }
                else if (other.gameObject.CompareTag("PotionShop"))  //포션상점NPC
                {
                     
                }
                else if (other.gameObject.CompareTag("Enhancement")) //강화소NPC
                {

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemObject itemObject = other.gameObject.GetComponent<ItemObject>();

        if (itemObject != null)
        {
            _interactionLayerList.Remove(itemObject.ItemData.ObjName);
            _interactItemObejctList.Remove(itemObject);
            UpdateUI(); 
        }

        if (_nextScene != null)
        {
            _nextScene = string.Empty;
            NextSceneInfo = string.Empty;
        }
    }

    private void InitializeCollider()
    {
        _interactionLayerList.Clear();
        UpdateUI();
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

    public void DestroyItem()
    {
        if (_interactItemObejctList.Count > 0)
        {
            ItemObject itemObject = _interactItemObejctList[0];

            _interactItemObejctList.Remove(itemObject);
            _interactionLayerList.Remove(itemObject.ItemData.ObjName);

            itemObject.GetItem();
            //Destroy(itemObject.gameObject);
            UpdateUI();
        }
    }

    public void GoNextScene()
    {
        if(_nextScene != string.Empty)
        {
            LoadingSceneController.LoadScene(_nextScene);
            _nextScene = string.Empty;
        }
    }
}
