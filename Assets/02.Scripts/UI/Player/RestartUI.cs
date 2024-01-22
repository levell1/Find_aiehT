using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartUI : BaseUI
{
    [SerializeField] private Image _backimage;
    [SerializeField] private GameObject _text;
    Color color;

    //죽으면 보이기
    private void OnEnable()
    {
        _text.SetActive(false);
        _backimage.color= new Color(0f, 0f, 0f, 0f);
        Color color = _backimage.color;
        StartCoroutine(backblur());
    }

    IEnumerator backblur() 
    {
        while (_backimage.color.a < 1f) 
        {
            yield return new WaitForSeconds(0.1f);
            color.a = color.a+0.05f ;
            _backimage.color = color;

        }
        _text.SetActive(true);
        yield return new WaitForSeconds(3f);
        
        base.CloseUI();
        //씬이동,초기화는 다른 스크립트에서.
    }
}
