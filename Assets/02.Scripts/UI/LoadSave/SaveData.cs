using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    private Button _saveButton;
    private SaveDataManager _saveDataManager;

    [HideInInspector] public List<ItemSlot> SaveInventory;
    [HideInInspector] public List<PotionInventorySlot> SavePotionInventory = new List<PotionInventorySlot>();

    [HideInInspector] public List<Quest> SaveQuest = new List<Quest>(); 
    [HideInInspector] public Dictionary<int, int> SaveEnemyQuestProgress = new Dictionary<int, int>(); 
    [HideInInspector] public Dictionary<int, int> SaveNatureQuestProgress = new Dictionary<int, int>(); 

    private void Awake()
    {
        _saveButton = GetComponent<Button>();
        _saveDataManager = GameManager.Instance.SaveDataManger;
    }

    private void Start()
    {
        _saveButton.onClick.AddListener(() => _saveDataManager.SavePlayerDataToJson());
    }
}
