using UnityEngine;


public class ShadowOfStormChargeBeamState : EnemyState
{

    private readonly ShadowOfStorm shadowOfStorm;

    public ShadowOfStormChargeBeamState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        shadowOfStorm = (ShadowOfStorm)_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        shadowOfStorm.ChargeBeamInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        shadowOfStorm.ChargeBeamInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        shadowOfStorm.ChargeBeamInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        shadowOfStorm.ChargeBeamInstance.DoAniamtionFinishTrigger();
    }
}