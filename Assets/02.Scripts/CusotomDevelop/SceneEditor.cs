using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneEditor : EditorWindow
{
    [MenuItem("Window/Custom Developer/Scene")]
    public static void Open()
    {
        GetWindow<SceneEditor>("씬 이동");
    }


    private void OnGUI()
    {
        GUILayout.Label("씬 이동 버튼", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        if (GUILayout.Button("Vilage Scene"))
        {
            GameManager.instance.Player.transform.position = new Vector3(-5, 0, 0);
            LoadingSceneController.LoadScene(SceneName.VillageScene);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Hunting Scene"))
        {
            GameManager.instance.Player.transform.position = new Vector3(-2, 0, 25);
            LoadingSceneController.LoadScene(SceneName.Field);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Tycoon Scene"))
        {
            GameManager.instance.Player.transform.position = new Vector3(5, 0, 8);
            LoadingSceneController.LoadScene(SceneName.TycoonScene);
        }

        GUILayout.EndVertical();

    }


}
