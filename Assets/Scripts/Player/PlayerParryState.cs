using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: set cooldown for parry, if parry success clear the cooldown
public class PlayerParryState : PlayerState
{
    public PlayerParryState(Player _player, PlayerStateMachine _stateMachine, string _animParamName) : base(_player, _stateMachine, _animParamName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.isParryActive = true;

        stateTimer = .01f;
    }

    override public void Exit()
    {
        base.Exit();
        player.isParryActive = false;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer > 0)
        {
            player.SetZeroVelocity();
        }
        

        if (isTriggerCalled)
            stateMachine.ChangeState(player.Idle);
    }
}