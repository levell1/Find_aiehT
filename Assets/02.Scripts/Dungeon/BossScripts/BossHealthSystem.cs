using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;


public class BossHealthSystem : MonoBehaviour
{

    public float MaxHealth;
    public float Health;

    public event Action OnDie;
    public Action<float, float> OnChangeHpUI;

    public bool IsDead =false;
    private SkinnedMeshRenderer[] meshRenderers;
    private Animator _animation;
    

    private void Awake()
    {
        _animation = gameObject.GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }
    private void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        MaxHealth = 210;
        Health = MaxHealth;
        OnChangeHpUI?.Invoke(Health, MaxHealth);
    }

    public void TakeDamage(float damage)
    {

        if (Health == 0) return;
        _animation.SetBool(AnimationParameterName.BossHit, true);
        Health = Mathf.Max(Mathf.Floor(Health - damage), 0);
        OnChangeHpUI?.Invoke(Health, MaxHealth);
        StartCoroutine(DamageFlash());
        Invoke("Animation", 0.1f);
        if (Health/MaxHealth == 0.1f)
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
            propBlock.SetColor("_Color", new Color(0.6f, 1f, 0.6f));
            meshRenderers[i].SetPropertyBlock(propBlock);
        }
    }
}
