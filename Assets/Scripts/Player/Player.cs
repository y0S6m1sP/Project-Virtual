using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public bool IsBusy { get; private set; }

    [Header("Basic Stats")]
    public float moveSpeed;
    public float jumpForce;
    public float wallJumpForce;
    public float jumpCantMoveDuration;
    public float rollDuration;
    public float rollSpeed;
    public bool isParryActive;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState Idle { get; private set; }
    public PlayerMoveState Move { get; private set; }
    public PlayerAttack1State Attack1 { get; private set; }
    public PlayerAttack2State Attack2 { get; private set; }
    public PlayerJumpState Jump { get; private set; }
    public PlayerFallState Fall { get; private set; }
    public PlayerRollState Roll { get; private set; }
    public PlayerWallSlideState WallSlide { get; private set; }
    public PlayerWallJumpState WallJump { get; private set; }
    public PlayerAirAttackState AirAttack { get; private set; }
    public PlayerParryState Parry { get; private set; }
    public PlayerDeadState Dead { get; private set; }

    public SkillManager Skill { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        Idle = new PlayerIdleState(this, StateMachine, "Idle");
        Move = new PlayerMoveState(this, StateMachine, "Move");
        Attack1 = new PlayerAttack1State(this, StateMachine, "Attack1");
        Attack2 = new PlayerAttack2State(this, StateMachine, "Attack2");
        Jump = new PlayerJumpState(this, StateMachine, "Jump");
        Fall = new PlayerFallState(this, StateMachine, "Jump");
        Roll = new PlayerRollState(this, StateMachine, "Roll");
        WallSlide = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJump = new PlayerWallJumpState(this, StateMachine, "Jump");
        AirAttack = new PlayerAirAttackState(this, StateMachine, "AirAttack");
        Parry = new PlayerParryState(this, StateMachine, "Parry");
        Dead = new PlayerDeadState(this, StateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        Skill = SkillManager.instance;
        StateMachine.Initialize(Idle);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;
        yield return new WaitForSeconds(_seconds);
        IsBusy = false;
    }

    public void OpenParryWindow()
    {
        isParryActive = true;
    }

    public void CloseParryWindow()
    {
        isParryActive = false;
    }

    public IEnumerator ParrySuccess(EntityStats _enemyStats)
    {
        AudioManager.instance.PlaySFX(Random.Range(0, 2));

        if (impulseSource != null)
            CameraShakeManager.Instance.CameraShake(impulseSource);

        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.1f); 
        Time.timeScale = 1f; 

        SwordManager.Instance.GenerateSword(transform, _enemyStats);

        yield return new WaitForSeconds(0.2f);
        StateMachine.ChangeState(Idle);
    }

    public void AnimFinishTrigger()
    {
        StateMachine.CurrentState.AnimFinishTrigger();
    }

    public void AnimAllowCancel()
    {
        StateMachine.CurrentState.AnimAllowCancel();
    }

    public void Die()
    {
        StateMachine.ChangeState(Dead);
    }

}
