using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

namespace EnemyStates
{
    public enum State { Idle, Patrol, Find, Fire, SoundCheck, Return, Size }

    public abstract class PatrolState : StateBase<State, PatrolMan>
    {
        protected EnemyWeaponHolder weaponHolder { get { return owner.weaponHolder; } }
        protected FieldOfView fov { get { return owner.fov; } }
        protected NavMeshAgent agent { get { return owner.agent; } }
        protected Transform transform { get { return owner.transform; } }
        protected GameObject player { get { return owner.player; } }
        protected Transform rayPoint { get { return owner.rayPoint; } }
        protected LayerMask obstacleMask { get { return owner.obstacleMask; } }
        protected Vector3 originPosition { get { return owner.originPosition; } }
        protected Vector3 soundPoint { get { return owner.soundPoint; } }
        protected Vector3 returnPoint { get { return owner.returnPoint; } }
        protected Vector3 randomPatrolPoint { get { return owner.randomPatrolPoint; } set { owner.randomPatrolPoint = value; } }
        protected bool isListen { get { return owner.isListen; } set { owner.isListen = value; } }
        protected bool isFind { get { return owner.isFind; } set { owner.isFind = value; } }
        protected Animator anim { get { return owner.anim; } set { owner.anim = value; } }

        protected PatrolState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }
    }

    public abstract class RifleState : StateBase<State, RifleMan>
    {
        protected EnemyWeaponHolder weaponHolder { get { return owner.weaponHolder; } }
        protected FieldOfView fov { get { return owner.fov; } }
        protected NavMeshAgent agent { get { return owner.agent; } }
        protected Transform transform { get { return owner.transform; } }
        protected GameObject player { get { return owner.player; } }
        protected Rig aimRig { get { return owner.aimRig; } }
        protected Vector3 originLookDir { get { return owner.originLookDir; } }
        protected Animator anim { get { return owner.anim; } set { owner.anim = value; } }
        protected bool isFind { get { return owner.isFind; } set { owner.isFind = value; } }

        protected RifleState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }
    }
}
