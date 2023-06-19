using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolIdleState : PatrolState
{
    float patrolTime;

    public PatrolIdleState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        patrolTime = 0f;
        anim.SetFloat("MoveSpeed", 0f);
    }

    public override void Update()
    {
        
    }

    public override void Transition()
    {
        patrolTime += Time.deltaTime;
        if (!fov.IsFinded())
        {
            if (patrolTime > 2f)
            {
                agent.isStopped = false;
                stateMachine.ChangeState(State.Patrol);
            }
        }
        else
        {
            agent.isStopped = true;
            stateMachine.ChangeState(State.Find);
        }
    }

    public override void Exit()
    {

    }
}
