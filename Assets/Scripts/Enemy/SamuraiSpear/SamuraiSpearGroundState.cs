using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearGroundState : EnemyState
{

    protected SamuraiSpear enemy;

    protected Transform player;

    public SamuraiSpearGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }


    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            stateMachine.ChangeState(enemy.Chase);
    }

}
