using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    private void SaveGameData()
    {

        //SaveEnemyQuestProgress.Clear();
        //SaveNatureQuestProgress.Clear();
        //SaveQuest.Clear();

        //_questManager = GameManager.Instance.QuestManager;

        //foreach (Quest quest in _questManager.AcceptQuestList)
        //{
        //    SaveQuest.Add(quest);
        //    Debug.Log(quest.QuestNumber);
        //}

        //foreach (var quest in _questManager.EnemyQuantityDict)
        //{
        //    SaveEnemyQuestProgress.Add(quest.Key, quest.Value);
        //    Debug.Log("Key: " + quest.Key + ", Value: " + quest.Value); // 디버그 로그에 키와 값을 출력
        //}

        //foreach (var quest in _questManager.NatureQuantityDict)
        //{
        //    SaveNatureQuestProgress.Add(quest.Key, quest.Value);
        //    Debug.Log("Key: " + quest.Key + ", Value: " + quest.Value); // 디버그 로그에 키와 값을 출력
        //}

        //_player = GameManager.Instance.Player.GetComponent<Player>();

        //_potionInventory = _player.GetComponent<PotionInventory>();

        //foreach(var item in _potionInventory.Potions)
        //{
        //    Debug.Log(item.PotionSO.Name);
        //    Debug.Log(item.PotionAmount.text);
        //}

        //_inventory = _player.Inventory;

        //foreach (var item in _inventory.Slots)
        //{
        //    SaveInventory.Add(item);
        //}

        //for(int i = 0; i < SaveInventory.Count; i++)
        //{
        //    Debug.Log(SaveInventory[i].Item.ObjName);
        //}

        //Debug.Log(_player.transform.position);
        //_equipmentDatas = GameManager.Instance.Player.GetComponent<EquipmentDatas>();

        //foreach(var data in _equipmentDatas.EquipData)
        //{
        //    Debug.Log(data.Level);
        //}

        // playerSO = GameManager.Instance.Player.GetComponent<Player>().Data;

        // Debug.Log(playerSO.PlayerData.GetPlayerGold());

        // Debug.Log(playerSO.PlayerData.GetPlayerAtk());



    }
}
