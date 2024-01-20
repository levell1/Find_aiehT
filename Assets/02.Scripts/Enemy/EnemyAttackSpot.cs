using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpot : MonoBehaviour
{
    private EnemySO _enemySO;
    [SerializeField] private Collider myCollider; //본인
    [SerializeField] private float knockbackForce = 40f;
    public Collider Collider { get;  set; }

    private void Start()
    {
        _enemySO = GetComponentInParent<Enemy>().Data;
        Collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (other.CompareTag("Enemy")) return;

        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_enemySO.Damage);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;

                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.VelocityChange);
            }


        }
    }
}
