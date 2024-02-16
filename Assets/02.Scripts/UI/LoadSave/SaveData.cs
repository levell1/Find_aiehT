using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    private Button _saveButton;
    private PlayerSO playerSO;

    private void Awake()
    {
        _saveButton = GetComponent<Button>();
    }

    private void Start()
    {
        _saveButton.onClick.AddListener(() => SaveGameData());
    }

    private void SaveGameData()
    {
        playerSO = GameManager.Instance.Player.GetComponent<Player>().Data;

        Debug.Log(playerSO.PlayerData.GetPlayerGold());

        //Debug.Log(playerSO.PlayerData.GetPlayerAtk());

    }
}
