using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommaText : MonoBehaviour
{
    private TMP_Text _glodText;
    //FIX
    //테스트
    public int _Value;

    private void Awake()
    {
        _glodText = GetComponent<TMP_Text>();
        //FIX
        //골드값 바뀔 때 += _Value
        _Value = 412200;
    }
    private void Update()
    {
        _glodText.text = GetComma(_Value).ToString();
    }
    public string GetComma(int data)
    {
        return string.Format("{0:#,###}", data); 
    }
    
}
