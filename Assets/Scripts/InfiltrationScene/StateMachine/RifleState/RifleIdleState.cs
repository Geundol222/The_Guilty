using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RifleIdleState : RifleState
{
    public RifleIdleState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        anim.SetBool("IsFind", false);
    }

    public override void Update()
    {
        
    }

    public override void Transition()
    {
        if (fov.IsFind)
        {
            stateMachine.ChangeState(State.Find);
        }
    }

    public override void Exit()
    {

    }
}
