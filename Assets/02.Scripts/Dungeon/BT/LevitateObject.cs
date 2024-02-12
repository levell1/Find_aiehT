using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateObject : MonoBehaviour
{
    public bool EndSkill =true;
    private float _time = 0;

    private void OnEnable()
    {
        EndSkill = false;
        _time = 0;
        GameManager.Instance.EffectManager.PlayStaminaEffect();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time<5)
        {
            EndSkill = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(TagName.Player)&& EndSkill == false)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 20);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EndSkill = true;
        //이펙트 종료
    }
}
