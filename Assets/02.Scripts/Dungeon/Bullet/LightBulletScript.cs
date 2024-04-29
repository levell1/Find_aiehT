using UnityEngine;

public class LightBulletScript : MonoBehaviour
{
    private Transform _player;
    private Rigidbody _rigidbody;
    readonly private float _lightDamage =500f;
    private float _delayTime = 0;
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
        _rigidbody.velocity = transform.forward * 6f;
        var ballTargetRotation = Quaternion.LookRotation(_player.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, ballTargetRotation, Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_lightDamage);
        }
        else if (other.gameObject.CompareTag(TagName.Wall))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _delayTime += Time.deltaTime;
        if (other.gameObject.TryGetComponent(out HealthSystem health)&& _delayTime > 0.5f)
        {
            health.TakeDamage(_lightDamage);
            _delayTime = 0;
        }
    }

    private void ReturnForSec() 
    {
        Destroy(gameObject);
    }
}
