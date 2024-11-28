using UnityEngine;

[CreateAssetMenu(fileName = "Attack-ShadowOfStorm-ChargeBeam", menuName = "Enemy/Behavior/Attack/ShadowOfStorm/ChargeBeam")]
class ShadowOfStormAttackChargeBeam : BaseEnemyAttackSO
{
    public override void DoUpdate()
    {
        base.DoUpdate();
        enemy.SetZeroVelocity();
        if (triggerCalled)
            enemy.StateMachine.ChangeState(enemy.Chase);
    }
}