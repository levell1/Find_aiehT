using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpot : MonoBehaviour
{
    private EnemySO _enemySO;
    [SerializeField] private Collider myCollider; //본인
    public Collider Collider { get;  set; }

    private float knockbackForce = 20f;

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
                // 플레이어의 현재 위치와 적의 위치를 기반으로 방향을 계산
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;

                // 플레이어의 방향으로 넉백을 가해줌
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }


        }
    }
}
