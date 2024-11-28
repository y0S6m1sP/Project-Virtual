public class DarkArcherAttackSpecialState : EnemyState
{
    private readonly DarkArcher darkArcher;

    public DarkArcherAttackSpecialState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        darkArcher = (DarkArcher)_enemy;
    }

    public override void Enter()
    {
        base.Enter();
        darkArcher.AttackSpecialInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        darkArcher.AttackSpecialInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        darkArcher.AttackSpecialInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        darkArcher.AttackSpecialInstance.DoAniamtionFinishTrigger();
    }
}