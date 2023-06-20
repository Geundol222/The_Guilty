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
        waitTime = 0;
        agent.speed = 15f;
    }

    public override void Update()
    {
        waitTime += Time.deltaTime;
    }

    public override void Transition()
    {
        if (waitTime > 3f)
        {
            isListen = false;
            stateMachine.ChangeState(State.Patrol);
        }
            
    }

    public override void Exit()
    {

    }
}
