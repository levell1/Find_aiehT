using System;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private DataManager _dataManager;
    private Vector3 _itemSpawnPoint;
    private Rigidbody _itemRigidbody;

    public ItemSO ItemData;

    public int ItemIndex;
    public bool IsActive;

    public event Action OnInteractionNatureItem;
    public static event Action<int> OnQuestTargetInteraction;

    private void Awake()
    {
        _itemRigidbody = GetComponent<Rigidbody>();
        _dataManager = GameManager.Instance.DataManager;
        _itemSpawnPoint = transform.position;

        if (ItemData.type == ItemType.NATUREITEM && _dataManager.ItemWaitSpawnDict.ContainsKey(ItemIndex))
        {
            IsActive = _dataManager.ItemWaitSpawnDict[ItemIndex];
            if (IsActive)
            {
                _dataManager.ItemWaitSpawnDict.Remove(ItemIndex);
            }
        }
    }

    private void OnEnable()
    {
        _itemRigidbody.velocity = Vector3.zero;
        transform.position = _itemSpawnPoint;
        
        if (ItemData.type == ItemType.NATUREITEM && !IsActive)
        {
            gameObject.SetActive(false);
        }
    }

    public void GetItem()
    {
        GameManager.Instance.Inventory.AddItem(ItemData);

        if (ItemData.type == ItemType.DROPITEM)
        {
            Destroy(gameObject);
        }
        else if (ItemData.type == ItemType.NATUREITEM)
        {
            IsActive = false;
            _dataManager.AddItems(ItemIndex, IsActive);
            gameObject.SetActive(false);
            OnInteractionNatureItem?.Invoke();
            OnQuestTargetInteraction?.Invoke(ItemData.ItemID);
        }
    }
}
