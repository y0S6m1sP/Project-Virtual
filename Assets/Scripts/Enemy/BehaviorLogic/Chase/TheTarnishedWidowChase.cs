using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-TheTarnishedWidow", menuName = "Enemy/Behavior/Chase/TheTarnishedWidow")]
public class TheTarnishedWidowChase : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.CanAttackSpecial2())
        {
            enemy.StateMachine.ChangeState(enemy.Special2);
            return;
        }

        if (enemy.IsPlayerInAttackDistance())
        {
            if (enemy.CanAttackSpecial1())
            {
                enemy.StateMachine.ChangeState(enemy.Special1);
                return;
            }

            if (enemy.CanAttack())
            {
                enemy.StateMachine.ChangeState(enemy.Attack);
                return;
            }
        }

        if (player.position.x > enemy.transform.position.x)
            chaseDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            chaseDir = -1;

        if (enemy.IsPlayerInAttackDistance())
        {
            enemy.SetZeroVelocity();
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, enemy.Rb.velocity.y);
    }
}
