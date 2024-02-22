using UnityEngine;

public class EnemyAttackSpot : MonoBehaviour
{
    private Enemy _enemy;
    private EnemySO _enemySO;
    [SerializeField] private Collider myCollider; 
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
        }
    }
}
