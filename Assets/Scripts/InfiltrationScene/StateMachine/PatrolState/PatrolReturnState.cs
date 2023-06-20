using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolReturnState : PatrolState
{
    public PatrolReturnState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {

    }

    public override void Update()
    {
        agent.destination = returnPoint;
    }

    public override void Transition()
    {
        if (!fov.IsFind && Vector3.Distance(transform.position, returnPoint) < 0.1f)
        {
            stateMachine.ChangeState(State.Idle);
        }

        if (fov.IsFind)
        {
            stateMachine.ChangeState(State.Find);
        }

        if (isListen)
            stateMachine.ChangeState(State.SoundCheck);
    }

    public override void Exit()
    {

    }
}
