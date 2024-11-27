using UnityEngine;

public class DaggerMush : Enemy
{
    [SerializeField] private BaseEnemyAttackSO AttackAir;
    [SerializeField] private BaseEnemyAttackSO AttackThrow;

    public BaseEnemyAttackSO AttackAirInstance { get; private set; }
    public BaseEnemyAttackSO AttackThrowInstance { get; private set; }

    public DaggerMushAttackAirState Air { get; private set; }
    public DaggerMushAttackThrowState Throw { get; private set; }

    [Header("Special Attack Stats")]
    public float AttackAirCooldown;
    [HideInInspector] public float lastTimeAttackAir;

    protected override void Awake()
    {
        base.Awake();

        AttackAirInstance = Instantiate(AttackAir);
        AttackThrowInstance = Instantiate(AttackThrow);
        Air = new DaggerMushAttackAirState(this, StateMachine, "Air");
        Throw = new DaggerMushAttackThrowState(this, StateMachine, "Throw");
    }

    override protected void Start()
    {
        base.Start();
        
        AttackAirInstance.Init(gameObject, this);
        AttackThrowInstance.Init(gameObject, this);
    }

    public bool CanUseAttackAir()
    {
        if (Time.time >= lastTimeAttackAir + AttackAirCooldown && IsPlayerDetected().distance > attackDistance)
        {
            lastTimeAttackAir = Time.time;
            return true;
        }

        return false;
    }

}