using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleMan : Enemy
{
    StateMachine<State, RifleMan> rifleStateMachine;

    protected override void Awake()
    {
        base.Awake();

        rifleStateMachine = new StateMachine<State, RifleMan>(this);

        rifleStateMachine.AddState(State.Idle, new RifleIdleState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Find, new RifleFindState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Fire, new RifleFireState(this, rifleStateMachine));
    }

    private void Start()
    {
        rifleStateMachine.Setup(State.Idle);
    }

    private void Update()
    {
        rifleStateMachine.Update();
    }
}
