using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashForceReceiver : MonoBehaviour
{
    private Player _player;
   
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCoolTime = 5f;
    [SerializeField] private Image _coolTimeImage;
    [SerializeField] private GameObject _dashImage;

    private float _dashTime = 0f; 

    public bool IsDash;
    public bool IsCoolTime { get; private set; }

    private CoolTimeManager _coolTimeManager;

    private void Awake()
    {
        _player = GetComponent<Player>();
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

        if (IsDash)
        {
            _dashTime += Time.fixedDeltaTime;

            IsCoolTime = true;
            _dashImage.SetActive(true);

            if (_dashTime >= _dashDuration )
            {
                IsDash = false;
                _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.Dash, _dashCoolTime, _coolTimeImage);
            }
        }
        if (!IsCoolTime)
        {
            _dashImage.SetActive(false);
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
