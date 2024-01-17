using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpot : MonoBehaviour
{
    private EnemySO _enemySO;
    [SerializeField] private Collider myCollider; //본인

    private void Start()
    {
        _enemySO = GetComponentInParent<Enemy>().Data;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_enemySO.Damage);
        }
    }
}
