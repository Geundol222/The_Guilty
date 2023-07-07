using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFindState : RifleState
{
    public RifleFindState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        anim.SetBool("IsFind", true);
        aimRig.weight = 1f;
        owner.StartCoroutine(owner.LookRoutine(player.transform));
    }

    public override void Update()
    {
        Vector3 lookPoint = player.transform.position;
        lookPoint.y = transform.position.y;
        transform.LookAt(lookPoint);
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            stateMachine.ChangeState(State.Idle);
        }

        if (fov.IsFind)
        {
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {

    }
}
