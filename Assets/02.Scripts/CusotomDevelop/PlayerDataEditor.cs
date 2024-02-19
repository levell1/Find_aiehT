using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDataEditor : EditorWindow
{

    string _levelText;

    [MenuItem("Window/Custom Developer/PlayerDataEdit")]
    public static void Open()
    {
        GetWindow<PlayerDataEditor>("플레이어 데이터 ");
    }

    private void OnGUI()
    {
        GUILayout.Label("레벨 데이터", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        _levelText = EditorGUILayout.TextField("Type Text", _levelText);

        if(GUILayout.Button("Level UP"))
        {
            LevelUp();
        }

        GUILayout.EndVertical();

        GUILayout.Space(10);
    }

    private void LevelUp()
    {
        PlayerSO playerSO = FindObjectOfType<Player>().GetComponent<Player>().Data;

        PlayerData playerData = playerSO.PlayerData;

        if (playerSO != null && playerSO.PlayerData != null)
        {

            if (Int32.TryParse(_levelText, out int level))
            {
                playerData.PlayerLevel = level;
                playerSO.PlayerLevelData.ApplyNextLevelData(playerData, level);

            }
        }

    }
}
