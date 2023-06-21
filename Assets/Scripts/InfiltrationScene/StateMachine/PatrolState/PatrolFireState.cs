using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFireState : PatrolState
{
    float FireTime;

    public PatrolFireState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        FireTime = 2f;
    }

    public override void Update()
    {
        FireTime -= Time.deltaTime;

        if (FireTime < 0)
        {
            FireTime = 2f;
            weaponHolder.Fire();
        }
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
