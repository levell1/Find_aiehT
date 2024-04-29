
using UnityEngine;
using UnityEngine.AI;

public class CheckPlayerDistanceNode : Node
{
    private int _playerLayerMask = 1 << 9;
    private Transform _pigtransform;
    private Animator _animation;
    private float _distance;
    
    public CheckPlayerDistanceNode(Transform transform,float distance)
    {
        _pigtransform = transform;
        _distance = distance;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        var collider = Physics.OverlapSphere(_pigtransform.position, _distance, _playerLayerMask);
        if (collider.Length <= 0) return state = NodeState.Failure;
        _animation.SetBool(AnimationParameterName.BossWalk, false);
        return state = NodeState.Success;
    }
}

public class CheckPlayerDistanceNotNode : Node
{
    private int _playerLayerMask = 1 << 9;
    private Transform _pigtransform;
    private Animator _animation;
    private float _distance;

    public CheckPlayerDistanceNotNode(Transform transform, float distance)
    {
        _pigtransform = transform;
        _distance = distance;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        var collider = Physics.OverlapSphere(_pigtransform.position, _distance, _playerLayerMask);
        if (collider.Length > 0) return state = NodeState.Failure;
        _animation.SetBool(AnimationParameterName.BossWalk, false);
        return state = NodeState.Success;
    }
}

public class LevitateNode : Node
{
    private Animator _animation;
    private LevitateObject _levitateObject;
    private Transform _pigTransform;
    private Transform _player;
    private float _time;
    private float _cooltime;

    public LevitateNode(Transform pigTransform, Transform playerTransform, LevitateObject levitateObject, float coolTime)
    {
        _levitateObject = levitateObject;
        _pigTransform = pigTransform;
        _player = playerTransform;
        _animation = pigTransform.GetComponent<Animator>();
        _cooltime= coolTime;
    }

    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time < _cooltime)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
        }
        
        if (_time > _cooltime)
        {
            _levitateObject.gameObject.SetActive(true);
            if (_levitateObject.EndSkill == true)
            {
                _levitateObject.gameObject.SetActive(false);
                _time = 0;
                _animation.SetBool(AnimationParameterName.BossSpin, false);
                return state = NodeState.Success;
            }
            else
            {
                _animation.SetBool(AnimationParameterName.BossSpin, true);
                
                return state = NodeState.Running;
            }
        }
        return state = NodeState.Running;
    }
}

public class RangeAttackNode : Node
{
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    private float _bulletangle = 10f;
    private float _time = 0;
    private GameObject[] _bullet = new GameObject[3];
    public RangeAttackNode(Transform playerTransform, Transform transform)
    {
        _player = playerTransform;
        _pigTransform = transform;
        _animation = transform.GetComponent<Animator>();

    }
    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;

        if (_time <= 1f)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
            _animation.SetBool(AnimationParameterName.BossAttack, false);
        }
        else
        {
            Vector3 direction = _player.position - _pigTransform.position;
            direction.y = 0f;

            _animation.SetBool(AnimationParameterName.BossAttack, true);

            for (int i = 0; i < _bullet.Length; i++)
            {
                _bullet[i] = GameManager.Instance.PoolingManager.GetObject("Bullet");
                _bullet[i].transform.rotation = Quaternion.LookRotation(direction);
                // 테스트 해보기 _bullet[i].transform.Rotate(Vector3.up * _bulletangle * (float)(SysMath.Pow(-1, i)));
            }
            _bullet[1].transform.Rotate(Vector3.up * _bulletangle);
            _bullet[2].transform.Rotate(Vector3.down * _bulletangle);

            for (int i = 0; i < _bullet.Length; i++)
            {
                _bullet[i].transform.position = _pigTransform.position + _pigTransform.forward * 2;
                _bullet[i].GetComponent<Rigidbody>().velocity = _bullet[i].transform.forward * 5f;
            }

            _time = 0;
            return state = NodeState.Success;
        }
        return state = NodeState.Failure;
    }
}


public class LightAttack : Node
{
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    private float _time = 0;
    private float _waitTime;
    private GameObject _lighObject;
    public LightAttack(Transform playerTransform, Transform transform, float waitTime, GameObject lighObject)
    {
        _player = playerTransform;
        _pigTransform = transform;
        _animation = transform.GetComponent<Animator>();
        _waitTime = waitTime;
        _lighObject = lighObject;
    }
    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time <= _waitTime)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
            _animation.SetBool(AnimationParameterName.BossFly, true);
            return state = NodeState.Running;
        }
        else
        {
            GameObject lightObject = Object.Instantiate(_lighObject);
            lightObject.transform.rotation = Quaternion.identity;
            lightObject.transform.position = _pigTransform.position + _pigTransform.forward * 2;
            _animation.SetBool(AnimationParameterName.BossFly, false);
            _time = 0;
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Pig2);
            return state = NodeState.Success;
        }
    }
}

public class RunAwayNode : Node
{
    private Animator _animation;
    private Vector3 _randomPoint = Vector3.zero;
    private NavMeshAgent _agent;
    private float _beforeSpeed;
    private float _randomRange =10f;
    public RunAwayNode(Transform transform, NavMeshAgent agent, float beforeSpeed)
    {
        _animation = transform.GetComponent<Animator>();
        _agent = agent;
        _beforeSpeed = beforeSpeed;
        _randomPoint = GetRandomPositionOnNavMesh();
    }
    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _randomRange;
        randomDirection += _agent.gameObject.transform.position;
        randomDirection.y= _agent.gameObject.transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, _randomRange, NavMesh.AllAreas)) 
        {
            return hit.position;
        }
        else
        {
            return _agent.gameObject.transform.position;
        }

    }
    public override NodeState Evaluate()
    {
        _agent.SetDestination(_randomPoint);
        _animation.SetBool(AnimationParameterName.BossWalk, true);
        _agent.speed =5f;
        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
        {
            _randomPoint = GetRandomPositionOnNavMesh();
            _agent.speed = _beforeSpeed;
            _animation.SetBool(AnimationParameterName.BossWalk, false);
            return state = NodeState.Success; 
        }
        else
        {
            return state = NodeState.Running; 
        }
    }
}

public class GoToPlayerNode : Node
{
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    private NavMeshAgent _agent;
    private float _agentAttackSpeed = 10.0f;
    private float _time=0;
    private float _waitTime = 0;

    public GoToPlayerNode(Transform player, Transform transform, NavMeshAgent agent, float waitTime)
    {
        _player = player;
        _pigTransform = transform;
        _agent = agent;
        _waitTime = waitTime;
        _animation = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime) 
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossFear, true);
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
            _agent.speed = _agentAttackSpeed;
            _animation.SetBool(AnimationParameterName.BossFear, false);
            _animation.SetBool(AnimationParameterName.BossRun, true);

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _animation.SetBool(AnimationParameterName.BossRun, false);
                _time = 0;
                return state = NodeState.Success; 
            }else
            {
                return state = NodeState.Running; 
            }
        }
        return state = NodeState.Running; 
    }

}

public class DashToPlayer : Node
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private Animator _animation;
    private NavMeshAgent _agent;
    private float _time = 0;
    private float _waitTime = 0;
    private int _count = 0;
    private bool _hasDashed = false;
    private Vector3 _dashPosition = Vector3.zero;
    private BoxCollider _collider;

    public DashToPlayer(Transform player, Transform transform, NavMeshAgent agent, float waitTime)
    {
        _playerTransform = player;
        _pigTransform = transform;
        _agent = agent;
        _waitTime = waitTime;
        _animation = transform.GetComponent<Animator>();
        _collider = _pigTransform.gameObject.GetComponent<BoxCollider>();
    }

    public override NodeState Evaluate()
    {
        
        _time += Time.deltaTime;
        if (_time < _waitTime)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 3);
            _agent.SetDestination(_pigTransform.position);
            _animation.SetBool(AnimationParameterName.BossSit, true);
        }
        else if (_waitTime <= _time && _time <= _waitTime+0.5f)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 3);
            _animation.SetBool(AnimationParameterName.BossSit, false);
            _animation.SetBool(AnimationParameterName.BossRoll, true);
        }
        else
        {
            if (!_hasDashed)
            {
                DashTowardsPlayer();
            }

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _hasDashed = false;
                _count++;
                if (_count > Random.Range(2, 5)|| _pigTransform.gameObject.name == "ChickBullet(Clone)")
                {
                    _collider.enabled = false;
                    _count = 0;
                    _time = 0;
                    _animation.SetBool(AnimationParameterName.BossRun, false);
                    return state = NodeState.Success;
                }
            }
            else
            {
                return state = NodeState.Running;
            }
        }
        return state = NodeState.Running;
    }

    private void DashTowardsPlayer()
    {
        for (int i = 4; i >= 0; i--)
        {
            _dashPosition = _playerTransform.position + ((_playerTransform.position - _pigTransform.position).normalized * i * 10);
            _dashPosition.y = _pigTransform.position.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(_dashPosition, out hit, 1f, NavMesh.AllAreas))
            {
                GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Pig1);
                _agent.SetDestination(_dashPosition);
                _collider.enabled = true;
                _animation.SetBool(AnimationParameterName.BossRoll, false);
                _animation.SetBool(AnimationParameterName.BossRun, true);
                break;
            }
        }
        _hasDashed = true;
    }
}
