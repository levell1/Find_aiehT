using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private BossHealthSystem _greenpigHealthSystem;
    private BossHealthSystem _AightHealthSystem;
    [SerializeField] private Light _light;
    [SerializeField] private AiehtAI _aieht;
    [SerializeField] private GameObject _pigs;
    private void Awake()
    {
        _AightHealthSystem = _aieht.GetComponent<BossHealthSystem>();
        _greenpigHealthSystem.OnDie += RespawnNextBoss;

        _AightHealthSystem.OnDie += Ending;
    }

    private void Ending()
    {
        Debug.Log("엔딩");
    }

    private void RespawnNextBoss()
    {
        StartCoroutine(BossEffect());
    }


    private IEnumerator BossEffect() 
    {
        var pigsAgent = _pigs.GetComponentsInChildren<NavMeshAgent>();
        
        while (_light.gameObject.transform.localPosition.y > 1f) 
        {

            _light.gameObject.transform.position +=Vector3.down*0.09f;
            if (_light.intensity < 500)
            {
                _light.intensity += 5;
            }
            if (_light.range > 5)
            {
                _light.range -= 1f;
            }

            foreach (NavMeshAgent agent in pigsAgent)
            {
                agent.SetDestination(_pigs.transform.position);
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(1f);
        _pigs.SetActive(false);
        _aieht.gameObject.SetActive(true);

        while (_light.intensity > 200 )
        {
            _light.intensity -= 3;

            if (_light.range < 150)
            {
                _light.range += 1f;
            }
            if (_light.gameObject.transform.localPosition.y < 5f)
            {
                _light.gameObject.transform.position += Vector3.up * 0.09f;
            }
            
            if (_light.color.g>0f)
            {
                Color currentcolor = _light.color;
                _light.color = currentcolor - new Color(0, 0.1f, 0, 0f);
                yield return new WaitForSecondsRealtime(0.01f);
            }
            
        }
    }
}
