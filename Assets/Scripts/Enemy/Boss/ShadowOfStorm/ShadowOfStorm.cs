using UnityEngine;

public class ShadowOfStorm : Enemy
{
    [SerializeField] private BaseEnemyAttackSO AttackChargeBeam;

    public BaseEnemyAttackSO ChargeBeamInstance { get; private set; }

    public ShadowOfStormChargeBeamState ChargeBeam { get; private set; }

    [Header("Special Attack Stats")]
    public float ChargeBeamCooldown;
    [HideInInspector] public float lastTimeChargeBeam;

    protected override void Awake()
    {
        base.Awake();

        ChargeBeamInstance = Instantiate(AttackChargeBeam);
        ChargeBeam = new ShadowOfStormChargeBeamState(this, StateMachine, "ChargeBeam");
    }

    override protected void Start()
    {
        base.Start();
        
        ChargeBeamInstance.Init(gameObject, this);
    }

    public bool CanUseChargeBeam()
    {
        if (Time.time >= lastTimeChargeBeam + ChargeBeamCooldown)
        {
            lastTimeChargeBeam = Time.time;
            return true;
        }

        return false;
    }

}