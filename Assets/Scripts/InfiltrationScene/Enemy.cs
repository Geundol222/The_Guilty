using EnemyStates;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    public ParticleSystem muzzleEffect;
    public Transform muzzlePoint;
    public GameObject player;
    public int damage;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public FieldOfView fov;

    protected virtual void Awake()
    {
        damage = 10;
        fov = GetComponentInChildren<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

    }
}
