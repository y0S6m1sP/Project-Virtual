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
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float closestEnemyCheckRadius = 25;

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

        // AutoAttack();
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

        // Fx.CreateParryFX(transform);
        SetupKnockbackDir(_enemyStats.transform);
        SetupKnockbackPower(_enemyStats.knockbackPower);
        Knockback();

        if (impulseSource != null)
            CameraShakeManager.Instance.CameraShake(impulseSource);

        _enemyStats.TakePhysicalDamage(Stats);

        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(0.3f);
        Time.timeScale = 1f;

        // if(Stats.currentMana > 0) {
        //     Stats.DecreaseManaBy(1);
        //     SwordManager.Instance.GenerateSword(transform, _enemyStats.transform);
        // }

        yield return new WaitForSeconds(0.05f);
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

    private Transform FindClosestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, closestEnemyCheckRadius, whatIsEnemy);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            EntityStats enemyStats = hit.GetComponent<EntityStats>();
            if (enemyStats != null && !enemyStats.IsDead)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void AutoAttack()
    {
        StartCoroutine(AutoAttackRoutine());
    }


    private IEnumerator AutoAttackRoutine()
    {
        while (!Stats.IsDead)
        {
            SwordManager.Instance.GenerateSword(transform, FindClosestEnemy());

            // Check the player's mana and adjust the wait time accordingly
            float waitTime = GetComponent<EntityStats>().currentMana > 0 ? 1f : 2f;

            yield return new WaitForSeconds(waitTime);
        }
    }

}
