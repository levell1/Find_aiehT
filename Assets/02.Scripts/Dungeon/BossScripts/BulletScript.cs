using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
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

    
}
