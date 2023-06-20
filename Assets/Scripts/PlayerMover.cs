using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float walkStepRange;
    [SerializeField] float runStepRange;

    private Animator anim;
    private Camera mainCam;
    private NavMeshAgent agent;
    private RaycastHit hit;
    private bool isWalk;
    private float lastStepTime = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, hit.point) < 1f)
        {
            anim.SetFloat("MoveSpeed", 0f);
        }

        lastStepTime -= Time.deltaTime;
        if (lastStepTime < 0)
        {
            lastStepTime = 0.5f;
            GenerateStepSound();
        }
    }

    private void OnMove(InputValue value)
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            if (groundMask.IsContain(hit.collider.gameObject.layer))
            {
                anim.SetFloat("MoveSpeed", 1f);
                agent.destination = hit.point;
            }
        }
        Move();
    }

    private void GenerateStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, isWalk ? walkStepRange : runStepRange);
        foreach (Collider collider in colliders)
        {
            IListenable listenable = collider.GetComponent<IListenable>();
            listenable?.Listen(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, walkStepRange);
        Gizmos.DrawWireSphere(transform.position, runStepRange);
    }
}
