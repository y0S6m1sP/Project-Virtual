using UnityEngine;

public class DarkArcher : Enemy
{
    [SerializeField] private BaseEnemyAttackSO AttackSpecial;

    public BaseEnemyAttackSO AttackSpecialInstance { get; private set; }

    public DarkArcherAttackSpecialState Special { get; private set; }

    [Header("Special Attack Stats")]
    public float AttackSpecialCooldown;
    [HideInInspector] public float lastTimeAttackSpecial;

    protected override void Awake()
    {
        base.Awake();

        AttackSpecialInstance = Instantiate(AttackSpecial);
        Special = new DarkArcherAttackSpecialState(this, StateMachine, "Special");
    }

    protected override void Start()
    {
        base.Start();

        AttackSpecialInstance.Init(gameObject, this);
    }

    public bool CanUseAttackSpecial()
    {
        if (Time.time >= lastTimeAttackSpecial + AttackSpecialCooldown)
        {
            lastTimeAttackSpecial = Time.time;
            return true;
        }

        return false;
    }

}