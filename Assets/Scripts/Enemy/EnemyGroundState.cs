using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundState : EnemyState
{

    protected Transform player;

    public EnemyGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
    {

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
