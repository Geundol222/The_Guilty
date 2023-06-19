using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFireState : PatrolState
{
    public PatrolFireState(PatrolMan owner, StateMachine<State, PatrolMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void Transition()
    {
        
    }

    public override void Exit()
    {

    }

    public void Fire()
    {
        muzzleEffect.Play();

        RaycastHit hit;

        if (Physics.Raycast(muzzlePoint.forward, player.transform.position, out hit, Vector3.Distance(muzzlePoint.position, player.transform.position), playerMask))
        {
            IHittable hittable = hit.transform.gameObject.GetComponent<IHittable>();
            ParticleSystem hitEffect = GameManager.Resource.Instantiate<ParticleSystem>("Particles/HitEffect", hit.point, Quaternion.LookRotation(hit.normal), true);
            hittable?.TakeDamage(damage);
        }
    }
}
