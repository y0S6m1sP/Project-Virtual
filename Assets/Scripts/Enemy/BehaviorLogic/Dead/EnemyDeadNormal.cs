using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dead-Normal", menuName = "Enemy/Behavior/Dead/Normal")]
public class EnemyDeadNormal : BaseEnemyDeadSO
{
    public override void DoEnter()
    {
        base.DoEnter();
        enemy.StartCoroutine(DestroyAfter(1f));
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        enemy.SetZeroVelocity();
    }
}
