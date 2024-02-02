using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeEditor : EditorWindow
{
    float timeIncrement = 0.04166667f;

    [MenuItem("Window/Custom Developer/Time")]
    public static void Open()
    {
        GetWindow<TimeEditor>("시간 이동");
    }

    private void OnGUI()
    {
        GUILayout.Label("시간 증가 버튼", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        timeIncrement = EditorGUILayout.FloatField("Time Increment", timeIncrement);

        if (GUILayout.Button("Time"))
        {
            GameManager.instance.GlobalTimeManager.DayTime += timeIncrement;
        }
        GUILayout.EndVertical();

    }

}
