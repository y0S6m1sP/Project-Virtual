using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Normal", menuName = "Enemy/Behavior/Attack/Normal")]
public class EnemyAttackNormal : BaseEnemyAttackSO
{
    public override void DoUpdate()
    {
        base.DoUpdate();
        enemy.SetZeroVelocity();
        if (triggerCalled) 
            enemy.StateMachine.ChangeState(enemy.Chase);
    }
}
