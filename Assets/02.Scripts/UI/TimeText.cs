using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TimeText : MonoBehaviour
{
    private TMP_Text _TimeText;

    float _min;
    float _hour;

    

    private void Awake()
    {
        
        _TimeText = GetComponent<TMP_Text>();
        //FIX
    }
    private void Update()
    {
        

        _TimeText.text = now.ToString("tt h:mm:ss"));
    }

    private void timer() 
    {
        _min += Time.deltaTime*5/60;
        if (_min>60)
        {
            _hour++;
            _min = 0;
        }
    }

    public string GetTime(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
