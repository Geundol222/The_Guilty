using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] BaseScene curScene;
    [SerializeField] bool debug;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float walkStepRange;
    [SerializeField] float runStepRange;

    private PlayerInteractor interactor;
    private FootStepRender wave;
    private bool isWalk;
    private Animator anim;
    private Camera mainCam;
    private NavMeshAgent agent;
    private RaycastHit hit;
    private bool isDiscovered = false;
    private bool isCrouching = false;
    private float originWalkStepRange;
    private float originRunStepRange;


    private void Awake()
    {
        interactor = GetComponent<PlayerInteractor>();
        wave = GetComponentInChildren<FootStepRender>();
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        originWalkStepRange = walkStepRange;
        originRunStepRange = runStepRange;
    }

    private void Update()
    {
        if (curScene.name == "InfiltrationScene")
        {
            Move();
            Crouch();
        }
        else if (curScene.name == "RoomScene")
            Move();
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, hit.point) < 0.5f)
        {
            if (curScene.name == "InfiltrationScene")
            {
                anim.SetFloat("MoveSpeed", 0f);
                wave.anim.SetFloat("MoveSpeed", 0f);
            }
            else if (curScene.name == "RoomScene")
            {
                anim.SetFloat("WalkSpeed", 0f);
            }
        }
    }

    private void OnMove(InputValue value)
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (curScene.name == "InfiltrationScene")
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask) && !interactor.IsHide)
            {
                wave.RenderMode(isWalk);

                if (groundMask.IsContain(hit.collider.gameObject.layer))
                {
                    if (isWalk && !interactor.IsHide)
                        anim.SetFloat("MoveSpeed", 1f);
                    else if (!isWalk && !interactor.IsHide)
                        anim.SetFloat("MoveSpeed", 3f);
                }

                agent.destination = hit.point;
            }
            Move();

            StartCoroutine(StepSoundRoutine());
        }
        else if (curScene.name == "RoomScene")
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                if (groundMask.IsContain(hit.collider.gameObject.layer))
                {
                    anim.SetFloat("WalkSpeed", 1f);
                }

                agent.destination = hit.point;
            }
            Move();
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

    private void Crouch()
    {
        if (curScene.name == "InfiltrationScene")
        {
            if (isCrouching)
            {
                isWalk = true;
                anim.SetBool("IsCrouch", true);
                agent.speed = 5f;
            }
            else
            {
                isWalk = false;
                anim.SetBool("IsCrouch", false);
                agent.speed = 15f;
            }
        }
    }

    private void OnCrouch(InputValue value)
    {
        isCrouching = !isCrouching;
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
