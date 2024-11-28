using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DaggerMush", menuName = "Enemy/Behavior/Chase/DaggerMush")]
class DaggerMushChase : BaseEnemyChaseSO
{
    private Transform RightBoundary;
    private Transform LeftBoundary;

    private int chaseDir;

    public override void Init(GameObject gameObject, Enemy enemy)
    {
        base.Init(gameObject, enemy);
        RightBoundary = GameObject.Find("RightBoundary").transform;
        LeftBoundary = GameObject.Find("LeftBoundary").transform;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();

        if (stateTimer > 0)
            return;

        if (enemy.IsPlayerDetected())
        {
            if (enemy.IsPlayerTooClose())
            {
                stateTimer = .1f;
                enemy.SetVelocity(-enemy.moveSpeed * 2 * chaseDir, 0);
                return;
            }
            
            if (enemy.CanAttackSpecial1() && enemy.IsPlayerDetected().distance > enemy.attackDistance)
            {
                enemy.StateMachine.ChangeState(enemy.Special1);
            }
            else if (enemy.CanAttack())
            {
                if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
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