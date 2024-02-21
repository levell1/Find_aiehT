using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    private Button _saveButton;
    private SaveDataManager _saveDataManager;

    /// Test
    private Player _player;
    private PlayerSO playerSO;
    private EquipmentDatas _equipmentDatas;
    private Inventory _inventory;
    private PotionInventory _potionInventory;
    private QuestManager _questManager;


    [HideInInspector] public List<ItemSlot> SaveInventory;
    [HideInInspector] public List<PotionInventorySlot> SavePotionInventory = new List<PotionInventorySlot>();

    [HideInInspector] public List<Quest> SaveQuest = new List<Quest>(); // 저장할 퀘스트들 가져옴
    [HideInInspector] public Dictionary<int, int> SaveEnemyQuestProgress = new Dictionary<int, int>(); // 퀘스트 진행상황 가져옴
    [HideInInspector] public Dictionary<int, int> SaveNatureQuestProgress = new Dictionary<int, int>(); // 퀘스트 진행

    private void Awake()
    {
        _saveButton = GetComponent<Button>();
        _saveDataManager = GameManager.Instance.SaveDataManger;
    }

    private void Start()
    {
        _saveButton.onClick.AddListener(() => _saveDataManager.SavePlayerDataToJson());
        //_saveButton.onClick.AddListener(() => SaveGameData());
    }

}
