using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFindState : PatrolState
{
    FindPlayerUI findUI;

    public PatrolFindState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {
        
    }

    public override void Enter()
    {
        agent.isStopped = true;
        findUI = GameManager.UI.ShowInGameUI<FindPlayerUI>("UI/InGameUI/FindPlayerUI");
        findUI.ShowFindUI(transform);
        anim.SetFloat("MoveSpeed", 0f);
        owner.StartCoroutine(owner.LookRoutine(player.transform));
    }

    public override void Update()
    {
        
    }

    public override void Transition()
    {
        if (!fov.IsFind)
            stateMachine.ChangeState(State.Idle);
        else
            stateMachine.ChangeState(State.Fire);
    }

    public override void Exit()
    {
        findUI.CloseFindUI();
    }
}
