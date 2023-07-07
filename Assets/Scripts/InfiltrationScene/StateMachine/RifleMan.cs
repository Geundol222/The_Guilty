using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RifleMan : Enemy
{
    [SerializeField] public Rig aimRig;

    StateMachine<State, RifleMan> rifleStateMachine;
    [HideInInspector] public Vector3 originLookDir;

    protected override void Awake()
    {
        base.Awake();

        rifleStateMachine = new StateMachine<State, RifleMan>(this);

        rifleStateMachine.AddState(State.Idle, new RifleIdleState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Find, new RifleFindState(this, rifleStateMachine));
        rifleStateMachine.AddState(State.Fire, new RifleFireState(this, rifleStateMachine));

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
