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
        FireTime = 2f;
    }

    public override void Update()
    {
        FireTime -= Time.deltaTime;

        if (FireTime < 0)
        {
            FireTime = 2f;
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
    }

    public override void Exit()
    {

    }

    public void Fire()
    {
    }
}
