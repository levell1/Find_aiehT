using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runsParameterName = "Run";

    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParmaeterName = "Jump";
    // TODO
    [SerializeField] private string _fallParameterName = "Fall";
    [SerializeField] private string _dashParameterName = "Dash";

    [SerializeField] private string _attackParameterName = "@Attack";
    //TODO
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    //TODO
    public int FallParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    //TODO
    public int ComboAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runsParameterName);

        AirParameterHash = Animator.StringToHash(_airParameterName);
        JumpParameterHash = Animator.StringToHash(_jumpParmaeterName);
        //TODO
        FallParameterHash = Animator.StringToHash(_fallParameterName);
        DashParameterHash = Animator.StringToHash(_dashParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        //TODO
        ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);
    }

}

