using EnemyStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleFireState : RifleState
{
    float FireTime;

    public RifleFireState(RifleMan owner, StateMachine<State, RifleMan> stateMachine) : base(owner, stateMachine) { }

    public override void Setup()
    {

    }

    public override void Enter()
    {
        FireTime = 1.5f;
    }

    public override void Update()
    {
        FireTime -= Time.deltaTime;

        if (FireTime < 0 && !playerController.IsDead)
        {
            FireTime = 1.5f;
            GameManager.Sound.PlaySound("Audios/InfiltrationScene/RifleShotSound", Audio.SFX, 0.3f);
            weaponHolder.Fire();
        }
    }

    public override void Transition()
    {
        if (!fov.IsFind)
        {
            stateMachine.ChangeState(State.Idle);
        }

        if (playerController.IsDead)
            stateMachine.ChangeState(State.Idle);
    }

    public override void Exit()
    {

    }

    public void Fire()
    {
    }
}
