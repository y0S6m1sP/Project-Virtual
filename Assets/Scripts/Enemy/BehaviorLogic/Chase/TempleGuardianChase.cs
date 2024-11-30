using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-TempleGuardian", menuName = "Enemy/Behavior/Chase/TempleGuardian")]
public class TempleGuardianChase : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.IsPlayerInAttackDistance())
        {
            if (enemy.CanAttackSpecial1())
            {
                enemy.StateMachine.ChangeState(enemy.Special1);
            }
            else if (enemy.CanAttack())
            {
                enemy.StateMachine.ChangeState(enemy.Attack);
            }
        }

        if (player.position.x > enemy.transform.position.x)
            chaseDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            chaseDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, enemy.Rb.velocity.y);
    }
}
