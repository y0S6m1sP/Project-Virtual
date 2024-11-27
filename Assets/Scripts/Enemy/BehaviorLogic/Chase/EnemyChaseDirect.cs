using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct", menuName = "Enemy/Behavior/Chase/Direct")]
public class EnemyChaseDirect : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.chaseTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (enemy.CanAttack())
                {
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

        if(enemy.IsPlayerInAttackDistance())
        {
            enemy.SetZeroVelocity();
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, enemy.Rb.velocity.y);
    }
}
