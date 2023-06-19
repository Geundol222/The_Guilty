using EnemyStates;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [HideInInspector] public EnemyWeaponHolder weaponHolder;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public FieldOfView fov;

    protected virtual void Awake()
    {
        weaponHolder = GetComponentInChildren<EnemyWeaponHolder>();
        fov = GetComponentInChildren<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }
}
