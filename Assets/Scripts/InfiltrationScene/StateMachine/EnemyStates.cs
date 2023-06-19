using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyStates
{
    public enum State { Idle, Patrol, Find, Fire, SoundCheck, Return, Size }

    public abstract class PatrolState : StateBase<State, PatrolMan>
    {
        protected EnemyWeaponHolder weaponHolder { get { return owner.weaponHolder; } }
        protected FieldOfView fov { get { return owner.fov; } }
        protected NavMeshAgent agent { get { return owner.agent; } }
        protected Transform transform { get { return owner.transform; } }
        protected Transform player {  get { return owner.player; } }
        protected Vector3 returnPoint { get { return owner.returnPoint; } }
        protected int patrolIndex { get { return owner.patrolIndex; } set { owner.patrolIndex = value; } }
        protected Transform[] patrolPoints { get { return owner.patrolPoints; } set { owner.patrolPoints = value; } }
        protected Animator anim { get { return owner.anim; } set { owner.anim = value; } }

        protected PatrolState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }
    }

    public abstract class RifleState : StateBase<State, RifleMan>
    {
        protected EnemyWeaponHolder weaponHolder { get { return owner.weaponHolder; } }
        protected FieldOfView fov { get { return owner.fov; } }
        protected NavMeshAgent agent { get { return owner.agent; } }
        protected Transform transform { get { return owner.transform; } }
        protected Transform player { get { return owner.player; } }
        protected Animator anim { get { return owner.anim; } set { owner.anim = value; } }

        protected RifleState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }
    }
}
