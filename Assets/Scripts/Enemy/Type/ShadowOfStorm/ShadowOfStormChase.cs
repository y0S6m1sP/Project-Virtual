using UnityEngine;

[CreateAssetMenu(fileName = "Chase-ShadowOfStorm", menuName = "Enemy/Behavior/Chase/ShadowOfStorm")]
class ShadowOfStormChase : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.chaseTime;

            if (enemy is ShadowOfStorm shadowOfStorm)
            {
                if (shadowOfStorm.CanUseChargeBeam())
                {
                    enemy.StateMachine.ChangeState(shadowOfStorm.ChargeBeam);
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