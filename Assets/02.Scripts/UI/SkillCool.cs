using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCool : MonoBehaviour
{
    private Image _coolTimeImage;

    private void Awake()
    {
        _coolTimeImage = GetComponent<Image>();
        //이벤트 추가

    }

    private void Update()
    {
        //테스트용 스킬쓰는 시점에 코루틴
        if (Input.GetKeyUp(KeyCode.Q)) 
        {
            StartCoroutine(CoolTime(3.0f));
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
    }
}
