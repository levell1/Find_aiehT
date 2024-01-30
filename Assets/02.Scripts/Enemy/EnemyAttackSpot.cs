using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpot : MonoBehaviour
{
    private Enemy _enemy;
    private EnemySO _enemySO;
    [SerializeField] private Collider myCollider; //본인
    //[SerializeField] private float knockbackForce = 40f;
    public Collider Collider { get;  set; }

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemySO = GetComponentInParent<Enemy>().Data;
        Collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (other.CompareTag(TagName.Enemy)) return;

        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_enemy.EnemyDamage);
            //Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            //if (rb != null)
            //{
            //    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;

            //    rb.AddForce(knockbackDirection * knockbackForce, ForceMode.VelocityChange);
            //}


        }
    }
}
