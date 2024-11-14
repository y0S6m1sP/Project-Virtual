using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : Entity
{

    public float moveSpeed;
    public float idleTime;
    public float moveTime;

    public EnemyStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

}
