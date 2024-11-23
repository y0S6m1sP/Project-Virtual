using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Basic Stats")]
    public float moveSpeed;
    public float idleTime;
    public float moveTime;
    public float chaseTime;

    [Header("Attack Stats")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    [HideInInspector] public bool canBeParrried;

    public EnemyStateMachine StateMachine { get; private set; }

    [Header("Behavior")]
    [SerializeField] private BaseEnemyIdleSO BaseEnemyIdle;
    [SerializeField] private BaseEnemyChaseSO BaseEnemyChase;
    [SerializeField] private BaseEnemyAttackSO BaseEnemyAttack;
    [SerializeField] private BaseEnemyDeadSO BaseEnemyDead;

    public BaseEnemyIdleSO BaseEnemyIdleInstance { get; private set; }
    public BaseEnemyChaseSO BaseEnemyChaseInstance { get; private set; }
    public BaseEnemyAttackSO BaseEnemyAttackInstance { get; private set; }
    public BaseEnemyDeadSO BaseEnemyDeadInstance { get; private set; }


    public EnemyIdleState Idle { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyAttackState Attack { get; private set; }
    public EnemyDeadState Dead { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        BaseEnemyIdleInstance = Instantiate(BaseEnemyIdle);
        BaseEnemyChaseInstance = Instantiate(BaseEnemyChase);
        BaseEnemyAttackInstance = Instantiate(BaseEnemyAttack);
        BaseEnemyDeadInstance = Instantiate(BaseEnemyDead);

        StateMachine = new EnemyStateMachine();

        Idle = new EnemyIdleState(this, StateMachine, "Idle");
        Attack = new EnemyAttackState(this, StateMachine, "Attack");
        Chase = new EnemyChaseState(this, StateMachine, "Move");
        Dead = new EnemyDeadState(this, StateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();

        BaseEnemyIdleInstance.Init(gameObject, this);
        BaseEnemyChaseInstance.Init(gameObject, this);
        BaseEnemyAttackInstance.Init(gameObject, this);
        BaseEnemyDeadInstance.Init(gameObject, this);

        StateMachine.Initialize(Idle);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

    public bool CanAttack()
    {
        if (Time.time >= lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }

    public void OpenParryWindow()
    {
        canBeParrried = true;
        StartCoroutine(ShowParryHint());
    }

    public void CloseParryWindow()
    {
        canBeParrried = false;
    }

    private IEnumerator ShowParryHint()
    {
        Sr.material = Resources.Load<Material>("Outline");
        yield return new WaitForSeconds(0.2f);
        Sr.material = new Material(Shader.Find("Sprites/Default"));
    }

    public virtual void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, 50, whatIsPlayer);

    public bool IsPlayerInAttackDistance()
    {
        return IsPlayerDetected() && IsPlayerDetected().distance < attackDistance;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * FacingDir, transform.position.y));
    }

    public virtual void Die()
    {
        StateMachine.ChangeState(Dead);
        GameLevelManager.instance.NextLevel(2f);
    }

}
