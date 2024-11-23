using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-StandStill", menuName = "Enemy/Behavior/Idle/StandStill")]
public class EnemyIdleStandStill : BaseEnemyIdleSO
{
    public override void DoEnter()
    {
        base.DoEnter();
        enemy.SetZeroVelocity();
    }
}
