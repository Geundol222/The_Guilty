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
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float walkStepRange;
    [SerializeField] float runStepRange;

    private Animator anim;
    private Camera mainCam;
    private NavMeshAgent agent;
    private RaycastHit hit;
    private bool isWalk;
    private bool isDiscovered = false;
    private float originWalkStepRange;
    private float originRunStepRange;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        originWalkStepRange = walkStepRange;
        originRunStepRange = runStepRange;
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

        // lastStepTime -= Time.deltaTime;
        // if (lastStepTime < 0 && !isDiscovered)
        // {
        //     lastStepTime = 0.5f;
        //     GenerateStepSound();
        // }
        // else if (lastStepTime < 0 && isDiscovered)
        // {
        //     lastStepTime = 0.5f;
        // }
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

        StartCoroutine(StepSoundRoutine());
    }

    private void GenerateStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, isWalk ? walkStepRange : runStepRange, enemyMask);
        
        foreach (Collider collider in colliders)
        {
            isDiscovered = true;
            IListenable listenable = collider.GetComponent<IListenable>();
            listenable?.Listen(transform.position);
        }
    }

    IEnumerator StepSoundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (isDiscovered)
            {
                yield return new WaitForSeconds(10f);
                walkStepRange = originWalkStepRange;
                runStepRange = originRunStepRange;
                isDiscovered = false;
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, isWalk ? walkStepRange : runStepRange, enemyMask);

                foreach (Collider collider in colliders)
                {
                    isDiscovered = true;
                    IListenable listenable = collider.GetComponent<IListenable>();
                    listenable?.Listen(transform.position);
                    walkStepRange *= 0.5f;
                    runStepRange *= 0.5f;
                }
            }

            yield return null;
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
