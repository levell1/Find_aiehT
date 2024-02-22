using System.Collections;
using UnityEngine;
using System;
public class BossHealthSystem : MonoBehaviour
{
    private float _maxHealth;
    public float Health;

    public event Action OnDie;
    public Action<float, float> OnChangeHpUI;

    public bool IsDead =false;
    private SkinnedMeshRenderer[] _meshRenderers;
    private Animator _animation;
    

    private void Awake()
    {
        _animation = gameObject.GetComponent<Animator>();
        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        _maxHealth = 6000;
        //상속으로 다시
        if (gameObject.name=="Aieht")
        {
            _maxHealth = 8000;
        }
        Health = _maxHealth;
        OnChangeHpUI?.Invoke(Health, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (Health == 0) return;
        _animation.SetBool(AnimationParameterName.BossHit, true);
        Health = Mathf.Max(Mathf.Floor(Health - damage), 0);
        OnChangeHpUI?.Invoke(Health, _maxHealth);

        StartCoroutine(DamageFlash());
        Invoke("Animation", 0.1f);

        if (gameObject.name == "GreenPig")
        {
            if (Health / _maxHealth <= 0.99f)
            {
                OnDie?.Invoke();
            }
        }

        if (gameObject.name == "Aieht" && Health <= 0)
        {
            OnDie?.Invoke();
        }
    }

    private void Animation() 
    {
        _animation.SetBool(AnimationParameterName.BossHit, false);
    }

    IEnumerator DamageFlash()
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        Color a = _meshRenderers[0].material.color;
        for (int i = 0; i < _meshRenderers.Length; i++)
        {

            _meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", new Color(1.0f, 0.4f, 0.4f));
            _meshRenderers[i].SetPropertyBlock(propBlock);
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", a);
            _meshRenderers[i].SetPropertyBlock(propBlock);
        }
    }
}
