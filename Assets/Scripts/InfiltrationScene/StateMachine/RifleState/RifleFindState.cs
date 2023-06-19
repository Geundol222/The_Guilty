using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFindState : RifleState
{
    float FireTime;

    public RifleFindState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        FireTime = 1.5f;
    }

    public override void Enter()
    {
        anim.SetFloat("MoveSpeed", 0f);
    }

    public override void Update()
    {
        FireTime -= Time.deltaTime;
    }

    public override void Transition()
    {
        if (!fov.IsFinded())
        {
            agent.isStopped = false;
            stateMachine.ChangeState(State.Patrol);
        }

        if (fov.IsFinded() && FireTime < 0f)
        {
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {

    }
}
