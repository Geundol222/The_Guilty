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
        agent.speed = 6f;
        agent.isStopped = false;
        agent.destination = returnPoint;
        anim.SetFloat("MoveSpeed", 1f);
    }

    public override void Update()
    {

    }

    public override void Transition()
    {
        if (!fov.IsFind && Vector3.Distance(transform.position, returnPoint) < 1f)
        {
            isFind = false;
            stateMachine.ChangeState(State.Idle);
        }

        if (fov.IsFind)
        {
            isFind = true;
            stateMachine.ChangeState(State.Find);
        }

        if (isListen)
            stateMachine.ChangeState(State.SoundCheck);
    }

    public override void Exit()
    {

    }
}
