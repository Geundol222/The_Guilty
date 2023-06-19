using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFireState : PatrolState
{
    public PatrolFireState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        Debug.Log("�߻�");
        weaponHolder.Fire();
    }

    public override void Update()
    {

    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            stateMachine.ChangeState(State.Idle);
        }
    }

    public override void Exit()
    {

    }
}