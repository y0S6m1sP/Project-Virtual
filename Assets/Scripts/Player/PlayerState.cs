using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private readonly string animParamName;
    protected float stateTimer;
    protected bool isTriggerCalled;
    protected bool isAllowCancelAnim;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animParamName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animParamName = _animParamName;
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(animParamName, true);
        rb = player.Rb;
        isTriggerCalled = false;
        isAllowCancelAnim = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        // player.Anim.SetFloat("yVelocity", rb.velocity.y);

    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animParamName, false);
    }

    public virtual void AnimFinishTrigger()
    {
        isTriggerCalled = true;
    }

    public virtual void AnimAllowCancel()
    {
        isAllowCancelAnim = true;
    }

}
