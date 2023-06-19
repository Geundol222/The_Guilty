using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFindState : PatrolState
{
    FindPlayerUI findUI;
    float FireTime;

    public PatrolFindState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        FireTime = 0f;
        findUI = GameManager.UI.ShowInGameUI<FindPlayerUI>("UI/FindPlayerUI");
        findUI.ShowFindUI(transform);
        anim.SetFloat("MoveSpeed", 0f);
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
            findUI.CloseFindUI();
            stateMachine.ChangeState(State.Idle);
        }

        if (fov.IsFind && FireTime > 2f)
        {
            stateMachine.ChangeState(State.Fire);
        }
    }

    public override void Exit()
    {

    }
}
