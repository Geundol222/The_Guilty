using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSoundCheckState : PatrolState
{
    float waitTime;

    public PatrolSoundCheckState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        agent.isStopped = false;
        anim.SetFloat("MoveSpeed", 1f);
        waitTime = 3f;
        agent.speed = 10f;
        agent.destination = soundPoint;
    }

    public override void Update()
    {
        if (Vector3.Distance(transform.position, soundPoint) < 0.5f)
        {
            anim.SetFloat("MoveSpeed", 0f);
            waitTime -= Time.deltaTime;
        }
    }

    public override void Transition()
    {
        if (fov.IsFind)
        {
            isFind = true;
            isListen = false;
            stateMachine.ChangeState(State.Find);
        }

        if (waitTime < 0)
        {
            isFind = false;
            isListen = false;
            stateMachine.ChangeState(State.Return);
        }
            
    }

    public override void Exit()
    {

    }
}
