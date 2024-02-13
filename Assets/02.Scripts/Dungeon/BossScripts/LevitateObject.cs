using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevitateObject : MonoBehaviour
{
    public bool EndSkill =true;
    private float _time = 0;
    private ParticleSystem _particle;

    private void Awake()
    {
        var particles = GameManager.Instance.EffectManager.GreenPigEffect.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particles.Length; i++)
        {
            var mains = particles[i].main;
            mains.startColor = new Color(0.6f, 1f, 0.6f);
        }
    }

    private void OnEnable()
    {
        EndSkill = false;
        _time = 0;
        
        GameManager.Instance.EffectManager.GreenPigLevitate(gameObject.transform);
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time>5)
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

        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(5);
        }
    }

}
