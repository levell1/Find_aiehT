using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runsParameterName = "Run";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _baseAttackParameterName = "BaseAttack";

    [SerializeField] private string _dieParameterName = "Die";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public int DieParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runsParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        BaseAttackParameterHash = Animator.StringToHash(_baseAttackParameterName);
        DieParameterHash = Animator.StringToHash(_dieParameterName);
    }
}
