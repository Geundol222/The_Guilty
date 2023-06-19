using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatrolMan : Enemy
{
    [SerializeField] public Transform[] patrolPoints;

    [HideInInspector] public Vector3 returnPoint;
    [HideInInspector] public int patrolIndex;

    StateMachine<State, PatrolMan> patrolStateMachine;

    protected override void Awake()
    {
        base.Awake();

        patrolStateMachine = new StateMachine<State, PatrolMan>(this);

        patrolStateMachine.AddState(State.Idle, new PatrolIdleState(this, patrolStateMachine));
        patrolStateMachine.AddState(State.Patrol, new PatrolPatrolState(this, patrolStateMachine));
        patrolStateMachine.AddState(State.Fire, new PatrolFireState(this, patrolStateMachine));
    }

    private void Start()
    {
        returnPoint = transform.position;
        patrolStateMachine.Setup(State.Idle);
    }

    private void Update()
    {
        patrolStateMachine.Update();
    }
}
