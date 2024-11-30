using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-GlitchSamurai", menuName = "Enemy/Behavior/Chase/GlitchSamurai")]
public class GlitchSamuraiChase : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.CanAttackSpecial1())
        {
            enemy.StateMachine.ChangeState(enemy.Special1);
            return;
        }

        if (enemy.IsPlayerInAttackDistance())
        {
            if (enemy.CanAttack())
            {
                enemy.StateMachine.ChangeState(enemy.Attack);
            }
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
