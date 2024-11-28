public class DaggerMushAttackAirState : EnemyState
{

    private readonly DaggerMush daggerMush;

    public DaggerMushAttackAirState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        daggerMush = (DaggerMush)_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        daggerMush.AttackAirInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        daggerMush.AttackAirInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        daggerMush.AttackAirInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        daggerMush.AttackAirInstance.DoAniamtionFinishTrigger();
    }
}
