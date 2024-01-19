using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;

    private void Awake()
    {
        //string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        //string jsonText = File.ReadAllText(jsonFilePath);

        //PlayerJsonData playerJsonData = JsonUtility.FromJson<PlayerJsonData>(jsonText);

        //PlayerSO.SetPlayerData(playerJsonData.PlayerData);

        // LoadJson<PlayerJsonData>("PlayerData");

        PlayerJsonData playerJsonData = LoadJson<PlayerJsonData>("PlayerData");
        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
        
        PlayerSkillData skillData = LoadJson<PlayerSkillData>("PlayerSkillData");
        PlayerSO.SetPlayerSkillData(skillData);

        PlayerLevelData playerLevelData = LoadJson<PlayerLevelData>("LevelData");
        PlayerSO.SetPlayerLevelData(playerLevelData);


    }

    public T LoadJson<T>(string FilePath)
    {
        StringBuilder jsonFilePathBuilder = new StringBuilder("Assets/Resources/JSON/");
        jsonFilePathBuilder.Append(FilePath).Append(".json");
        string jsonFilePath = jsonFilePathBuilder.ToString();

        string jsonText = File.ReadAllText(jsonFilePath);

        return JsonUtility.FromJson<T>(jsonText);

    }

}
