using UnityEngine;

public class BaseEnemyIdleSO : BaseEnemyStateSO
{
    public override void DoUpdate()
    {
        base.DoUpdate();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            enemy.StateMachine.ChangeState(enemy.Chase);
    }
}