using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillWeapon : MonoBehaviour
{
    [SerializeField] private Collider _skillCollider;
    private Transform _throwPoint;

    private int _damage;

    private readonly List<Collider> _alreadyCollidedObjects = new List<Collider>();

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        InitializeCollider();
    }

    private void InitializeCollider()
    {
        _alreadyCollidedObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _skillCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if (other.TryGetComponent(out EnemyHealthSystem health))
        {
            health.TakeDamage(_damage);
        }

    }

    public void SetSkillAttack(int damage)
    {
        Vector3 throwDirection = transform.root.forward;

        


        this._damage = damage;
    }

    public void SkillDestroy()
    {
        Destroy(gameObject);
    }

}
