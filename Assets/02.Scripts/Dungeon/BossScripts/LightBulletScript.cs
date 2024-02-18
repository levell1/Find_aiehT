using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulletScript : MonoBehaviour
{
    private Transform _player;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        Invoke("ReturnForSec", 7f);
    }

    private void Update()
    {
        _rigidbody.velocity = transform.forward * 5f;
        var ballTargetRotation = Quaternion.LookRotation(_player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, ballTargetRotation, Time.deltaTime * 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(10);
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
            health.TakeDamage(10);
        }
    }

    private void ReturnForSec() 
    {
        GameManager.Instance.PoolingManager.ReturnObject(gameObject);
    }

}
