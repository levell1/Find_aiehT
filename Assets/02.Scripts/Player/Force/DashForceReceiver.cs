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

    //private int _maxStamina;
    //private int _stamina;

    //private Vector3 _dashStartPosition;
    //private Vector3 _dashTargetPosition;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _staminaSystem = _player.GetComponent<StaminaSystem>();

        Init();
    }

    public void Init()
    {
        IsCoolTime = false;
        IsDash = false;
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
                    StartCoroutine(CoolDown());
            }
            // 쿨타임을 위한 계산
            else if (IsCoolTime)
            {
                _coolTime += Time.fixedDeltaTime;

                if(_coolTime >= _dashCoolTime)
                {
                    IsCoolTime = false;
                    _coolTime = 0f;
                }

            }


        }    
    }

    //public bool CanUseDash(int dashStamina)
    //{
    //    return _stamina >= dashStamina;
    //}

    ///// 대쉬시 - 10;
    //public void UseDash(int dashStamina)
    //{
    //    if (_stamina == 0) return;
    //    _stamina = Mathf.Max(_stamina - dashStamina, 0);

    //    //Debug.Log("스태미너" + _stamina);
    //}

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

            //* -Mathf.Log(1 / Player.Rigidbody.drag)

            //_player.Rigidbody.velocity += dashPower;

            _player.Rigidbody.AddForce(dashPower, ForceMode.VelocityChange);

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
    }

    IEnumerator CoolDown()
    {
        IsCoolTime = true;
        yield return new WaitForSeconds(_dashCoolTime);
        IsCoolTime = false;
    }

}
