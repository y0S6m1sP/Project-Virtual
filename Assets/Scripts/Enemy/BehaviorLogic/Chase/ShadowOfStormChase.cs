using UnityEngine;

[CreateAssetMenu(fileName = "Chase-ShadowOfStorm", menuName = "Enemy/Behavior/Chase/ShadowOfStorm")]
class ShadowOfStormChase : BaseEnemyChaseSO
{
    private int chaseDir;

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (enemy.CanAttackSpecial1())
        {
            enemy.StateMachine.ChangeState(enemy.Special1);
        }
        else if (enemy.IsPlayerInAttackDistance())
        {
            enemy.StateMachine.ChangeState(enemy.Attack);
        }

        if (player.position.x > enemy.transform.position.x)
            chaseDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            chaseDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, enemy.Rb.velocity.y);
    }
}