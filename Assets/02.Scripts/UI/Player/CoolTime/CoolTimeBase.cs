using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeBase : MonoBehaviour
{
    protected Image _coolTimeImage;
    protected float _coolCount;
    protected PlayerSO _playerData;

    protected KeyCode _keyCode;
    protected bool _isPlaying;

    protected void Awake()
    {
        _coolTimeImage = GetComponent<Image>();
        if (_playerData == null)
        {
            _playerData = GameObject.FindWithTag(TagName.Player).GetComponent<Player>().Data;
        }
        
    }

    protected void Update()
    {
        //테스트용 스킬쓰는 시점에 코루틴
        if (Input.GetKeyUp(_keyCode)&& !_isPlaying) 
        {
            _isPlaying = true;
            StartCoroutine(CoolTime(_coolCount));
        }
    }
    IEnumerator CoolTime(float cool) 
    {
        float cooltime=cool;
        while (cooltime > 0.1f) 
        {
            cooltime -= Time.deltaTime;
            _coolTimeImage.fillAmount = cooltime/cool;
            yield return null;
        }
        _coolTimeImage.fillAmount = 0;
        _isPlaying=false;
    }
}
