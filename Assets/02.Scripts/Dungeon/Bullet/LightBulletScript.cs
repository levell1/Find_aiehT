using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulletScript : MonoBehaviour
{
    private Transform _player;
    private Rigidbody _rigidbody;
    readonly private float _lightDamage =30f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.LightMagic,transform.position,0.1f);
        Invoke("ReturnForSec", 7f);
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * 6f;
        var ballTargetRotation = Quaternion.LookRotation(_player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, ballTargetRotation, Time.deltaTime * 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_lightDamage);
        }
        else if (other.gameObject.tag == TagName.Wall)
        {
            GameManager.Instance.PoolingManager.ReturnObject(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_lightDamage);
        }
    }

    private void ReturnForSec() 
    {
        GameManager.Instance.PoolingManager.ReturnObject(gameObject);
    }

}
