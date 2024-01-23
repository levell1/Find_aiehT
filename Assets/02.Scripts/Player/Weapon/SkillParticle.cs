using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticle : MonoBehaviour
{
    public Player _player;
    private ParticleSystem _particleSystem;
    Transform originalParent;

    private float _playtime = 0.6f;

    private Vector3 _localPosition;
    private Vector3 _localScale;
    private Quaternion _localRotation;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        originalParent = transform.parent;

        _localPosition = transform.localPosition;
        _localScale = transform.localScale;
        _localRotation = transform.localRotation;
    }

    public void PlayParticle()
    {
        StartCoroutine(DelayParticle());
    }

    IEnumerator DelayParticle()
    {
        gameObject.transform.parent = null;
        gameObject.transform.localScale = _localScale;

        yield return new WaitForSeconds(_playtime);

        _particleSystem.Play();

        yield return new WaitForSeconds(2f);

        transform.parent = originalParent;

        transform.localPosition = _localPosition;
        transform.localScale = _localScale;  
        transform.localRotation = _localRotation;
    }

}
