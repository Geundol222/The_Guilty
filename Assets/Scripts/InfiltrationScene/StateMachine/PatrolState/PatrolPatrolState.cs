using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPatrolState : PatrolState
{
    float randomRange;

    public PatrolPatrolState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
    }

    public override void Enter()
    {
        anim.SetFloat("MoveSpeed", 1f);
        randomRange = Random.Range(-10, 10);
    }

    public override void Update()
    {
        agent.destination = randomPatrolPoint;

        RaycastHit hit;

        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, 4f))
        {
            if (obstacleMask.IsContain(hit.collider.gameObject.layer))
            {
                randomRange = Random.Range(-10, 10);
                randomPatrolPoint = new Vector3(originPosition.x + randomRange, 0, originPosition.z + randomRange);
            }
        }
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            isFind = false;
            if (Vector3.Distance(randomPatrolPoint, transform.position) < 0.1f)
            {
                stateMachine.ChangeState(State.Idle);
            }
        }
        else
        {
            isFind = true;
            agent.isStopped = true;
            stateMachine.ChangeState(State.Find);
        }

        if (isListen)
            stateMachine.ChangeState(State.SoundCheck);
    }

    public override void Exit()
    {

    }
}
