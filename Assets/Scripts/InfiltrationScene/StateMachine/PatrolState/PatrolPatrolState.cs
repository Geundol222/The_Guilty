using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPatrolState : PatrolState
{
    public PatrolPatrolState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        anim.SetFloat("MoveSpeed", 1f);
    }

    public override void Update()
    {
        agent.destination = patrolPoints[patrolIndex].position;
    }

    public override void Transition()
    {
        if (!fov.IsFinded())
        {
            if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < 0.1f)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                stateMachine.ChangeState(State.Idle);
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
