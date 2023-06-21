using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFindState : RifleState
{
    FindPlayerUI findUI;
    float FireTime;

    public RifleFindState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        FireTime = 0;
        findUI = GameManager.UI.ShowInGameUI<FindPlayerUI>("UI/InGameUI/FindPlayerUI");
        findUI.ShowFindUI(transform);
        anim.SetBool("IsFind", true);        
        owner.StartCoroutine(owner.LookRoutine(player.transform));
    }

    public override void Update()
    {
        FireTime += Time.deltaTime;
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
