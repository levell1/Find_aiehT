using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public GameObject TakeDamageText;
    public GameObject TakeDamageText2;

    private Enemy _enemy;
    private EnemySO _enemySO;
    public float MaxHealth;
    public float Health;
    public Canvas HpCanvas;
    public Image HpBar;
    public TextMeshProUGUI EnemyName;
    public bool Hit;
    public int HitCool;
    public float DamageAmount;
    public event Action OnDie;
    public static event Action<int> OnQuestTargetDie;

    public bool IsDead;

    private Camera _camera;

    private SkinnedMeshRenderer[] meshRenderers;

    Coroutine _coroutine;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemySO = GetComponent<Enemy>().Data;
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        MaxHealth = _enemy.EnemyMaxHealth;
        Health = MaxHealth;
        EnemyName.text = string.Format("[" + _enemySO.EnemyName + "]");
        HpCanvas.gameObject.SetActive(false);
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Hit)
        {
            HpCanvas.gameObject.SetActive(true);
            //카메라 방향으로 캔버스 돌리기
            HpCanvas.transform.LookAt(HpCanvas.transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(HitCancel());
            }
        }
        else
        {
            HpCanvas.gameObject.SetActive(false);
        }

        HpBar.fillAmount = Health / MaxHealth;
    }


    public void TakeDamage(float damage)
    {
        Hit = true;
        if (Health == 0) return;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        DamageAmount = damage;
        if (TakeDamageText.activeSelf)
        {
            TakeDamageText2.SetActive(true);
        }
        TakeDamageText.SetActive(true);

        Health = Mathf.Max(Health - damage, 0);
        StartCoroutine(DamageFlash());

        _enemy._stateMachine.ChangeState(_enemy._stateMachine.ChasingState);

        if (Health == 0)
        {
            IsDead = true;
            OnDie.Invoke();
            OnQuestTargetDie?.Invoke(_enemySO.EnemyID);
        }
    }

    private IEnumerator HitCancel()
    {
        yield return new WaitForSeconds(HitCool); //해당시간동안 플레이어를 추격한다.
        Hit = false;
        _coroutine = null;
    }

    

    public IEnumerator DamageFlash()
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        Color a = meshRenderers[0].material.color;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            
            meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1.0f, 0.4f, 0.4f));
            meshRenderers[i].SetPropertyBlock(propBlock);
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", a);
            meshRenderers[i].SetPropertyBlock(propBlock);
        }

    }
}
