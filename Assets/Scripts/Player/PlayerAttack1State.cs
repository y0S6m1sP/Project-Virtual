using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerAttack1State : PlayerAttackState
{

    public PlayerAttack1State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetInteger("ComboCounter", 1);
    }

    public override void Update()
    {
        base.Update();

        if (isAllowCancelAnim && Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.Attack2);
    }
}