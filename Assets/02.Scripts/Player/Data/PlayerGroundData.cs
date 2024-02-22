using System;
using UnityEngine;

[Serializable]
public class PlayerGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 20f;

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 1f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 2f;

    [field: Header("DashData")]
    [field: SerializeField] [field: Range(0f, 50f)] public float DashForce { get; private set; } = 10f;
    [field: SerializeField] [field: Range(0, 20)] public int DashStamina { get; private set; } = 10;
    [field: SerializeField][field: Range(0, 10)] public int RegenerateStamina { get; private set; } = 1;
}
