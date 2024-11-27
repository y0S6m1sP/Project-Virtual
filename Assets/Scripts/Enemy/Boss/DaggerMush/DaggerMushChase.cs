using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DaggerMush", menuName = "Enemy/Behavior/Chase/DaggerMush")]
class DaggerMushChase : BaseEnemyChaseSO
{
    private Transform RightBoundary;
    private Transform LeftBoundary;

    private int chaseDir;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
        RightBoundary = GameObject.Find("RightBoundary").transform;
        LeftBoundary = GameObject.Find("LeftBoundary").transform;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.chaseTime;

            if (enemy is DaggerMush daggerMush)
            {
                // TODO: complete the logic
                if (player.position.x > RightBoundary.position.x || player.position.x < LeftBoundary.position.x)
                {
                    enemy.StateMachine.ChangeState(daggerMush.Throw);
                }

                if (daggerMush.CanUseAttackAir())
                {
                    enemy.StateMachine.ChangeState(daggerMush.Air);
                }
                else if (enemy.CanAttack())
                {
                    if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                        enemy.StateMachine.ChangeState(enemy.Attack);
                }
            }
        }
        else
        {
            if (stateTimer < 0)
                enemy.StateMachine.ChangeState(enemy.Idle);
        }

        if (player.position.x > enemy.transform.position.x)
            chaseDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            chaseDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, enemy.Rb.velocity.y);
    }
}