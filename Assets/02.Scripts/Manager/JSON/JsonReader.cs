using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public PlayerSO PlayerSO;

    private void Start()
    {
        // JSON 파일 경로 설정
        string jsonFilePath = "Assets/Resources/JSON/PlayerData.json";

        // JSON 파일에서 데이터 읽기
        string jsonText = File.ReadAllText(jsonFilePath);

        Debug.Log(jsonText);

        PlayerJsonData playerJsonData = JsonUtility.FromJson<PlayerJsonData>(jsonText);

        PlayerSO.SetPlayerData(playerJsonData.PlayerData);
    }
}
