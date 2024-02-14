using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashForceReceiver : MonoBehaviour
{
    private Player _player;
    private StaminaSystem _staminaSystem;
   
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolTime = 5f;

    private float _dashTime = 0f; // 대쉬를 하기위한 시간
    private float _coolTime= 0f; //  쿨타임을 계산하기위한 시간

    public bool IsDash;
    public bool IsCoolTime { get; private set; } // true => 쿨타임 중

    private CoolTimeManager _coolTimeManager;

    //private int _maxStamina;
    //private int _stamina;

    //private Vector3 _dashStartPosition;
    //private Vector3 _dashTargetPosition;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _staminaSystem = _player.GetComponent<StaminaSystem>();
        _coolTimeManager = GameManager.Instance.CoolTimeManger;

        Init();
    }

    public void Init()
    {
        IsCoolTime = false;
        IsDash = false;
        _coolTimeManager.AddCoolTimeEvent(CoolTimeObjName.Dash, HandleCoolTimeFinish);
    }

    void FixedUpdate()
    {
        if(IsDash)
        {
            _dashTime += Time.fixedDeltaTime;

            if (_dashTime >= _dashDuration )
            {
                IsDash = false;

                if (!IsCoolTime)
                    IsCoolTime = true;
                _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.Dash, _dashCoolTime, null);
            }
        }    
    }

    public void Dash(float dashForce)
    {
        if (_player.GroundCheck.IsGrounded() && !IsDash && !IsCoolTime)
        {
            IsDash = true;
            _dashTime = 0f;

            StartCoroutine(DashCoroutine(dashForce));

        }

    }

    IEnumerator DashCoroutine(float dashForce)
    {
        Vector3 dashDirection = transform.forward;
        Vector3 dashPower = dashDirection;

       while (_dashTime <= _dashDuration)
        {
            
            dashPower += dashDirection * dashForce;

            _player.Rigidbody.AddForce(dashPower, ForceMode.VelocityChange);

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }
    
    private void HandleCoolTimeFinish()
    {
        IsCoolTime = false;
    }
}
