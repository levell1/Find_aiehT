using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Tutorials;
    private int _index;

    private void Start()
    {
        _index = 0;

        if (Tutorials.Length == 0) return;

        foreach (var tutorial in Tutorials)
        {
            tutorial.SetActive(false);
        }

        StartTutorial(); //시작시 딜레이를 조금 주고 싶다면 인보크 고민
    }

    private void LateUpdate()
    {
        if (!Tutorials[_index].activeSelf)
        {
            ++_index;
            StartTutorial();
        }
    }

    private void StartTutorial()
    {
        if (_index >= Tutorials.Length) return;

        Tutorials[_index].SetActive(true);
    }

}
