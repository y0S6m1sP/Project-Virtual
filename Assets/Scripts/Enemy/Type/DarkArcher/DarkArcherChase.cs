using UnityEngine;

[CreateAssetMenu(fileName = "Chase-DarkArcher", menuName = "Enemy/Behavior/Chase/DarkArcher")]
class DarkArcherChase : BaseEnemyChaseSO
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

        if (enemy.transform.position.x <= LeftBoundary.position.x || enemy.transform.position.x >= RightBoundary.position.x)
        {
            enemy.transform.position = new Vector3((LeftBoundary.position.x + RightBoundary.position.x) / 2, enemy.transform.position.y, enemy.transform.position.z);
        }

        if (enemy is DarkArcher darkArcher)
        {
            if (darkArcher.CanUseAttackSpecial() && darkArcher.IsPlayerTooClose())
            {
                enemy.StateMachine.ChangeState(darkArcher.Special);
            }
            else if (enemy.IsPlayerInAttackDistance() && enemy.CanAttack())
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