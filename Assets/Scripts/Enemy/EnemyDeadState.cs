public class EnemyDeadState : EnemyState
{

    public EnemyDeadState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyDeadInstance.DoEnter();
        
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyDeadInstance.DoUpdate();
    }

    override public void Exit()
    {
        base.Exit();
        enemy.BaseEnemyDeadInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyDeadInstance.DoAniamtionFinishTrigger();
    }
}