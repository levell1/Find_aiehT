using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class LevitateObject : MonoBehaviour
{
    public bool EndSkill =true;
    private float _time = 0;
    private ParticleSystem _particle;
    readonly private float _staminaDamage =5f;
    readonly private float _delayTime = 0.1f;
    readonly private float _dagage = 50f;
    [SerializeField] bool _isBoss=false;

    private void Awake()
    {
        _particle = GameManager.Instance.EffectManager.CreateGreenPigLevitate(transform);
        _particle.gameObject.transform.localScale = Vector3.one * 1.5f;
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
        
        GameManager.Instance.EffectManager.GreenPigLevitate();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time>3)
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

        if (other.gameObject.TryGetComponent(out StaminaSystem stamina))
        {
            stamina.ReduceStamina(_staminaDamage, _delayTime);
        }

        if (other.gameObject.TryGetComponent(out HealthSystem healthSystem) && _isBoss == true)
        {
            healthSystem.TakeDamage(_dagage);
        }
    }

}