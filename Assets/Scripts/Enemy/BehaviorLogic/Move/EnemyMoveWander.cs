using UnityEngine;

[CreateAssetMenu(fileName = "Move-Wander", menuName = "Enemy/Behavior/Move/Wander")]
class EnemyMoveWnader : BaseEnemyMoveSO
{

    [SerializeField] private float wanderTime = 5f;

    public override void DoEnter()
    {
        base.DoEnter();
        stateTimer = wanderTime;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.IsPlayerDetected())
        {
            enemy.StateMachine.ChangeState(enemy.Chase);
            return;
        }

        if (stateTimer < 0)
        {
            enemy.StateMachine.ChangeState(enemy.Idle);
            return;
        }

        if (!enemy.IsGroundDetected())
        {
            enemy.Flip();
        }

        enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDir, enemy.Rb.velocity.y);
    }
}