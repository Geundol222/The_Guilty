using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFindState : RifleState
{
    float FireTime;

    public RifleFindState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        FireTime = 0;
        aimRig.weight = 1;
        owner.StartCoroutine(owner.LookRoutine(player.transform));
        anim.SetBool("IsFind", true);
    }

    public override void Update()
    {
        FireTime += Time.deltaTime;

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

        if (fov.IsFind && FireTime > 3f)
        {
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {

    }
}
