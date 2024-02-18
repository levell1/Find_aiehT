using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;
    private SaveDataManager _saveDataManager;

    /// 불러오기 (LoadJSON)
    // 1. new 게임인지 save게임인지 판별 (타이틀에서 새로하기 / 이어하기 버튼으로 판별하기)
    // 2. 저장된 JSON 다시 덮어주기


    private void Awake()
    {
        _saveDataManager = GameManager.Instance.SaveDataManger;
        //InitPlayerData();

        //PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>(JsonDataName.PlayerData);
        //PlayerSO.SetPlayerData(playerJsonData.PlayerData);

        PlayerSkillData skillData = LoadJson<PlayerSkillData>(JsonDataName.PlayerSkillData);
        PlayerSO.SetPlayerSkillData(skillData);

        PlayerLevelData playerLevelData = LoadJson<PlayerLevelData>(JsonDataName.PlayerLevelData);
        PlayerSO.SetPlayerLevelData(playerLevelData);

    }

    // 새로하기
    public void InitPlayerData()
    {
        PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>(JsonDataName.PlayerData);
        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
    }

    // 이어하기
    public void LoadPlayerData()
    {

    }

    public T LoadJson<T>(string FilePath)
    {
        StringBuilder jsonFilePathBuilder = new StringBuilder(ResourcePath.JsonLoadPath);
        jsonFilePathBuilder.Append(FilePath).Append(".json");
        string jsonFilePath = jsonFilePathBuilder.ToString();

        string jsonText = File.ReadAllText(jsonFilePath);

        return JsonUtility.FromJson<T>(jsonText);

    }

    public void SaveJson(object data, string filePath)
    {
        string jsonFilePath = Path.Combine(Application.persistentDataPath, filePath);
        Debug.Log(jsonFilePath);
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);
    }

}
