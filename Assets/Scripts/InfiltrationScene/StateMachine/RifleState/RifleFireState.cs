using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFireState : RifleState
{
    public RifleFireState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        anim.SetTrigger("Fire");
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
        else
        {
            stateMachine.ChangeState(State.Find);
        }
    }

    public override void Exit()
    {

    }

    public void Fire()
    {
    }
}
