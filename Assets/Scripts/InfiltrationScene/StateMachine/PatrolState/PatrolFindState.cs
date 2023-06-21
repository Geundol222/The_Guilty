using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFindState : PatrolState
{
    public PatrolFindState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        agent.isStopped = true;
        anim.SetFloat("MoveSpeed", 0f);
        owner.StartCoroutine(owner.LookRoutine(player.transform));
    }

    public override void Update()
    {
        
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            isFind = false;
            stateMachine.ChangeState(State.Idle);
        }
        else
        {
            isFind = true;
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {
        
    }
}
