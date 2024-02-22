using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runsParameterName = "Run";
    [SerializeField] private string _dashParameterName = "Dash";

    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParmaeterName = "Jump";
    [SerializeField] private string _fallParameterName = "Fall";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    [SerializeField] private string _skillParameterName = "@Skill"; 
    [SerializeField] private string _throwSkillParamterName = "ThrowSkill";
    [SerializeField] private string _spreadSkillParamterName = "SpreadSkill";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }

    public int SkillParameterHash { get; private set; }
    public int ThrowSkillParameterHash { get; private set; }
    public int SpreadSkillParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runsParameterName);

        AirParameterHash = Animator.StringToHash(_airParameterName);
        JumpParameterHash = Animator.StringToHash(_jumpParmaeterName);
        FallParameterHash = Animator.StringToHash(_fallParameterName);
        DashParameterHash = Animator.StringToHash(_dashParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);

        SkillParameterHash = Animator.StringToHash(_skillParameterName);
        ThrowSkillParameterHash = Animator.StringToHash(_throwSkillParamterName);
        SpreadSkillParameterHash = Animator.StringToHash(_spreadSkillParamterName);
    }
}