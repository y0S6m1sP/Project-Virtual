public class SamuraiSpearDeadState : EnemyState
{
    private readonly SamuraiSpear enemy;

    public SamuraiSpearDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.StartCoroutine(DestroyAfter(1f));
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
    }
}