using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RifleMan : Enemy
{
    StateMachine<State, RifleMan> rifleStateMachine;
    [HideInInspector] public Vector3 originLookDir;
    [HideInInspector] public MultiAimConstraint aimRig;

    protected override void Awake()
    {
        base.Awake();

        rifleStateMachine = new StateMachine<State, RifleMan>(this);

        rifleStateMachine.AddState(State.Idle, new RifleIdleState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Find, new RifleFindState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Fire, new RifleFireState(this, rifleStateMachine));

        aimRig = GetComponentInChildren<MultiAimConstraint>();
        originLookDir = transform.forward;
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
