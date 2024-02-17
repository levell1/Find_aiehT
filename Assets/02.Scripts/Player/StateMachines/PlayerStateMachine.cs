using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }

    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }

    public PlayerDashState DashState { get; }

    public PlayerComboAttackState ComboAttackState { get; }

    public PlayerThrowSkillState PlayerThrowSkillState { get; }
    public PlayerSpreadSkillState PlayerSpreadSkillState { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Vector3 PreviousPosition { get; set; }

    public float JumpForce { get; set; }
    public float DashForce { get; set; }
    public bool IsDashCoolTime { get; set; }

    public int MaxStamina { get; set; }
    public int Stamina { get; set; }
    public int DashStamina { get; set; }
    public int RegenStamina { get; set; }

    public int ComboIndex { get; set; }
    public bool IsAttacking { get; set; }

    public int SKillCost { get; set; }
    public bool IsSkillCoolTime { get; set; }

    public string SceneName { get; set; } 
    

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);

        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        DashState = new PlayerDashState(this);

        ComboAttackState = new PlayerComboAttackState(this);

        PlayerThrowSkillState = new PlayerThrowSkillState(this);
        PlayerSpreadSkillState = new PlayerSpreadSkillState(this);

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;

        PreviousPosition = player.transform.position;
    }
}

