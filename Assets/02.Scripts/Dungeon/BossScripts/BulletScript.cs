using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    private CapsuleCollider _collider;
    private ChickBulletBT _chickBullet;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _chickBullet = gameObject.GetComponent<ChickBulletBT>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _collider= gameObject.GetComponent<CapsuleCollider>();
    }
    private void OnEnable()
    {
        _chickBullet.enabled = false;
        _collider.enabled = false;
        _agent.enabled = false;
        
        StartCoroutine(init());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(20);
            GameManager.Instance.PoolingManager.ReturnObject(gameObject);
        }
        else if (other.gameObject.tag == TagName.Wall)
        {
            GameManager.Instance.PoolingManager.ReturnObject(gameObject);
        }
    }

    private IEnumerator init()
    {
        yield return new WaitForSeconds(0.1f);
        
        _collider.enabled = true;
        _agent.enabled = true;
        _chickBullet.enabled = true;
        yield return new WaitForSeconds(5f);
        _chickBullet.enabled = false;
        _collider.enabled = false;
        _agent.enabled = false;
        GameManager.Instance.PoolingManager.ReturnObject(gameObject);
    }
    

}