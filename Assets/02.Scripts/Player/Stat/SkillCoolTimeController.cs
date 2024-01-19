using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTimeController : MonoBehaviour
{
    private Player _player;
    private float _coolTime;

    public bool IsCoolTime { get; private set; }

    private void Start()
    {
        _player = GetComponent<Player>();
        IsCoolTime = false;
    }

    public void StartCoolTime(float coolTime)
    {
        if(!IsCoolTime)
        {
            _coolTime = coolTime;
            StartCoroutine(CoolTimeCoroutine());
        }
    }


    private IEnumerator CoolTimeCoroutine()
    {
        IsCoolTime = true;

        while (_coolTime > 0f)
        {
            yield return new WaitForSeconds(1f); // 1초 기다립니다 (게임의 요구에 따라 조절 가능)
            _coolTime--;

        }

        IsCoolTime = false;
    }

}
