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
        
    }

    public override void Enter()
    {
        anim.SetBool("IsFind", true);
        FireTime = 0;
    }

    public override void Update()
    {
        FireTime += Time.deltaTime;
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            stateMachine.ChangeState(State.Idle);
        }

        if (fov.IsFind && FireTime > 3f)
        {
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {

    }
}
