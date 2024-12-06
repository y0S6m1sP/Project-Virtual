using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-StandStill", menuName = "Enemy/Behavior/Idle/StandStill")]
public class EnemyIdleStandStill : BaseEnemyIdleSO
{

    [SerializeField] private float idleTime = 5f;

    public override void DoEnter()
    {
        base.DoEnter();
        stateTimer = idleTime;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        enemy.SetZeroVelocity();

        if (stateTimer < 0)
        {
            enemy.StateMachine.ChangeState(enemy.Move);
        }
    }
}
