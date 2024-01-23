using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadSkill : MonoBehaviour
{
    [SerializeField] private MeshCollider _skillRangeCollider;

    private int _damage;

    private readonly List<Collider> _alreadyCollidedObjects = new List<Collider>();

    private void Start()
    {
        DisableCollider();
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
        if (other == _skillRangeCollider) return;
        if (_alreadyCollidedObjects.Contains(other)) return;

        _alreadyCollidedObjects.Add(other);

        if (other.TryGetComponent(out EnemyHealthSystem health))
        {
            Debug.Log(_damage);
            health.TakeDamage(_damage);
        }

    }

    public void SetAttack(int damage)
    {
        this._damage = damage;
    }

    // 콜라이더를 활성화할 때 이 메서드 호출
    public void EnableCollider()
    {
        if (_skillRangeCollider != null)
        {
            _skillRangeCollider.enabled = true;
        }
    }

    // 콜라이더를 비활성화할 때 이 메서드 호출
    public void DisableCollider()
    {
        if (_skillRangeCollider != null)
        {
            _skillRangeCollider.enabled = false;
            InitializeCollider();
        }
    }
}
