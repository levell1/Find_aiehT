using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartUI : BaseUI
{
    [SerializeField] private Image _backImage;
    [SerializeField] private GameObject _text;
    private Color _color;

    //죽으면 보이기
    private void OnEnable()
    {
        _text.SetActive(false);
        _backImage.color= new Color(0f, 0f, 0f, 0f);
        _color = _backImage.color;
        StartCoroutine(BackBlur());
    }

    IEnumerator BackBlur() 
    {
        while (_backImage.color.a < 1f) 
        {
            yield return new WaitForSeconds(0.1f);
            _color.a = _color.a+0.05f ;
            _backImage.color = _color;
        }
        _text.SetActive(true);
        yield return new WaitForSeconds(3f);
        base.CloseUI();
        //씬이동,초기화는 다른 스크립트에서.
    }
}
