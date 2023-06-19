using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFindState : PatrolState
{
    float FireTime;

    public PatrolFindState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

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

        Vector3 dir = (player.transform.position - transform.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, 0.1f);
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
