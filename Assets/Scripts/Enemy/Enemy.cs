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
    [HideInInspector] public bool canBeParrried;
    public float tooCloseDistance;


    [Header("Attack Stats")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Special1 Attack Stats")]
    public float Special1Distance;
    public float Special1Cooldown;
    [HideInInspector] public float lastTimeSpecial1Attacked;

    [Header("Special2 Attack Stats")]
    public float Special2Distance;
    public float Special2Cooldown;
    [HideInInspector] public float lastTimeSpecial2Attacked;

    [Header("Special3 Attack Stats")]
    public float Special3Distance;
    public float Special3Cooldown;
    [HideInInspector] public float lastTimeSpecial3Attacked;

    [Header("Skill")]
    public GameObject SkillPrefab;

    public EnemyStateMachine StateMachine { get; private set; }

    [Header("Behavior")]
    [SerializeField] private BaseEnemyIdleSO BaseEnemyIdle;
    [SerializeField] private BaseEnemyChaseSO BaseEnemyChase;
    [SerializeField] private BaseEnemyAttackSO BaseEnemyAttack;
    [SerializeField] private BaseEnemyAttackSO BaseEnemyAttackSpecial1;
    [SerializeField] private BaseEnemyAttackSO BaseEnemyAttackSpecial2;
    [SerializeField] private BaseEnemyAttackSO BaseEnemyAttackSpecial3;
    [SerializeField] private BaseEnemyDeadSO BaseEnemyDead;

    public BaseEnemyIdleSO BaseEnemyIdleInstance { get; private set; }
    public BaseEnemyChaseSO BaseEnemyChaseInstance { get; private set; }
    public BaseEnemyAttackSO BaseEnemyAttackInstance { get; private set; }
    public BaseEnemyAttackSO BaseEnemyAttackSpecial1Instance { get; private set; }
    public BaseEnemyAttackSO BaseEnemyAttackSpecial2Instance { get; private set; }
    public BaseEnemyAttackSO BaseEnemyAttackSpecial3Instance { get; private set; }
    public BaseEnemyDeadSO BaseEnemyDeadInstance { get; private set; }

    public EnemyIdleState Idle { get; private set; }
    public EnemyChaseState Chase { get; private set; }
    public EnemyAttackState Attack { get; private set; }
    public EnemyAttackSpecial1State Special1 { get; private set; }
    public EnemyAttackSpecial2State Special2 { get; private set; }
    public EnemyAttackSpecial3State Special3 { get; private set; }
    public EnemyDeadState Dead { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        BaseEnemyIdleInstance = Instantiate(BaseEnemyIdle);
        BaseEnemyChaseInstance = Instantiate(BaseEnemyChase);
        BaseEnemyAttackInstance = Instantiate(BaseEnemyAttack);
        BaseEnemyDeadInstance = Instantiate(BaseEnemyDead);

        if (BaseEnemyAttackSpecial1 != null)
            BaseEnemyAttackSpecial1Instance = Instantiate(BaseEnemyAttackSpecial1);
        if (BaseEnemyAttackSpecial2 != null)
            BaseEnemyAttackSpecial2Instance = Instantiate(BaseEnemyAttackSpecial2);
        if (BaseEnemyAttackSpecial3 != null)
            BaseEnemyAttackSpecial3Instance = Instantiate(BaseEnemyAttackSpecial3);

        StateMachine = new EnemyStateMachine();

        Idle = new EnemyIdleState(this, StateMachine, "Idle");
        Attack = new EnemyAttackState(this, StateMachine, "Attack");
        Special1 = new EnemyAttackSpecial1State(this, StateMachine, "Special1");
        Special2 = new EnemyAttackSpecial2State(this, StateMachine, "Special2");
        Special3 = new EnemyAttackSpecial3State(this, StateMachine, "Special3");
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
        if (BaseEnemyAttackSpecial1 != null)
            BaseEnemyAttackSpecial1Instance.Init(gameObject, this);
        if (BaseEnemyAttackSpecial2 != null)
            BaseEnemyAttackSpecial2Instance.Init(gameObject, this);
        if (BaseEnemyAttackSpecial3 != null)
            BaseEnemyAttackSpecial3Instance.Init(gameObject, this);

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

    public bool CanAttackSpecial1()
    {
        if (Time.time >= lastTimeSpecial1Attacked + Special1Cooldown)
        {
            lastTimeSpecial1Attacked = Time.time;
            return true;
        }

        return false;
    }

    public bool CanAttackSpecial2()
    {
        if (Time.time >= lastTimeSpecial2Attacked + Special2Cooldown)
        {
            lastTimeSpecial2Attacked = Time.time;
            return true;
        }

        return false;
    }

    public bool CanAttackSpecial3()
    {
        if (Time.time >= lastTimeSpecial3Attacked + Special3Cooldown)
        {
            lastTimeSpecial3Attacked = Time.time;
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

    public bool IsPlayerTooClose()
    {
        return IsPlayerDetected() && IsPlayerDetected().distance < tooCloseDistance;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * FacingDir, transform.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + tooCloseDistance * FacingDir, transform.position.y));
    }

    public virtual void Die()
    {
        StateMachine.ChangeState(Dead);
        GameLevelManager.instance.NextLevel(2f);
    }

}
