using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerAirState
{
    public PlayerAirAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (isTriggerCalled)
            stateMachine.ChangeState(player.Fall);

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.Idle);

    }
}