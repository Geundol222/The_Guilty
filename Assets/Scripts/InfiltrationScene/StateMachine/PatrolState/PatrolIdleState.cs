using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolIdleState : PatrolState
{
    float patrolTime;
    float randomRange;

    public PatrolIdleState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        agent.isStopped = true;
        patrolTime = 0f;
        anim.SetFloat("MoveSpeed", 0f);
        randomRange = Random.Range(-10, 10);
    }

    public override void Update()
    {
        patrolTime += Time.deltaTime;
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            isFind = false;
            if (patrolTime > 2f)
            {
                randomPatrolPoint = new Vector3(originPosition.x + randomRange, 0, originPosition.z + randomRange);
                agent.isStopped = false;
                stateMachine.ChangeState(State.Patrol);
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
