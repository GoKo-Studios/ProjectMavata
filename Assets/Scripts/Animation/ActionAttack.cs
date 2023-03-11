using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Action", menuName = "ScriptableObject/Action/Attack")]
public class ActionAttack : ActionBase
{
    [SerializeField] private Tags m_tags;
    [SerializeField] private int m_damage;

    [Header("Hitbox Properties")]
    [Tooltip("Which type of hitbox is prioritized for hit detection.")]
    [SerializeField] private int m_priority;
    [Tooltip("Dictates how many times a move can hit. Set to 1 for single hit moves.")]
    [SerializeField] private int m_part = 1;

    [Header("Stun Properties")]
    [Tooltip("Stun inflicted upon hit (in frames).")]
    [SerializeField] private int m_hitStun;
    [SerializeField] private int m_blockStun;
    [SerializeField] private int m_freeze;

    [Header("Knockback Properties")]
    [SerializeField] private float m_knockup;
    [SerializeField] private float m_knockback;

    [Header("SFX Properties")]
    [SerializeField] private AudioClip m_sound;
    [SerializeField] private float m_soundLevel;

    [Header("VFX Properties")]
    [SerializeField] private Vector3 m_screenShakeVelocity;

    [Header("Frame Data")]
    [SerializeField] private int m_startFrames;
    [SerializeField] private int m_activeFrames;
    [SerializeField] private int m_recoveryFrames;

    [Header("Animation Clips")]
    [SerializeField] private AnimationClip m_meshAnimationS;
    [SerializeField] private AnimationClip m_meshAnimationA;
    [SerializeField] private AnimationClip m_meshAnimationR;
    [SerializeField] private AnimationClip m_boxAnimationS;
    [SerializeField] private AnimationClip m_boxAnimationA;
    [SerializeField] private AnimationClip m_boxAnimationR;

    public float AnimSpeedS {get{return AdjustAnimationTime(m_meshAnimationS, m_startFrames);}}
    public float AnimSpeedA {get{return AdjustAnimationTime(m_meshAnimationA, m_activeFrames);}}
    public float AnimSpeedR {get{return AdjustAnimationTime(m_meshAnimationR, m_recoveryFrames);}}

    public Tags Tags {get => m_tags;}
    public int Damage {get => m_damage;}
    public int Priority {get => m_priority;}
    public int Part {get => m_part;}
    public int HitStun {get => m_hitStun;}
    public int BlockStun {get => m_blockStun;}
    public int Freeze {get => m_freeze;}
    public float Knockup {get => m_knockup;}
    public float Knockback {get => m_knockback;}
    public AudioClip Sound {get => m_sound;}
    public float SoundLevel {get => m_soundLevel;}
    public Vector3 ScreenShakeVelocity {get => m_screenShakeVelocity;}

    public int StartFrames {get => m_startFrames;}
    public int ActiveFrames {get => m_activeFrames;}
    public int RecoveryFrames {get => m_recoveryFrames;}

    public AnimationClip MeshAnimationS {get => m_meshAnimationS;}
    public AnimationClip MeshAnimationA {get => m_meshAnimationA;}
    public AnimationClip MeshAnimationR {get => m_meshAnimationR;}
    public AnimationClip BoxAnimationS {get => m_boxAnimationS;}
    public AnimationClip BoxAnimationA {get => m_boxAnimationA;}
    public AnimationClip BoxAnimationR {get => m_boxAnimationR;}
}

[System.Flags]
public enum Tags 
{
    None = 0, //0000
    ShortRanged = 1 << 0, //0001 
    MidRanged = 1 << 1, //0010
    LongRanged = 1 << 2, //0100
    HighDamage = 1 << 3, //1000
    MidDamage = 1 << 4, 
    LowDamage = 1 << 5,
    Projectile = 1 << 6,
    SlowAnimation = 1 << 7,
    MidAnimation = 1 << 8,
    FastAnimation = 1 << 9
}
