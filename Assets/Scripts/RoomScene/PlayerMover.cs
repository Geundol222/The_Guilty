using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    float walkSpeed;
    Vector3 hitPoint;
    Vector3 lookDir;
    NavMeshAgent agent;
    Camera mainCam;
    Animator anim;
    bool isWalking;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        mainCam = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isWalking)
        {
            if (agent.velocity.magnitude == 0)
            {
                isWalking = false;
                anim.SetFloat("WalkSpeed", 0);
                return;
            }

            anim.SetFloat("WalkSpeed", 1);
            Vector3 dir = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
            anim.transform.forward = dir;
        }
    }

    private void OnMove(InputValue value)
    {
        RaycastHit hit;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            SetDirection(hit.point);
        }
        Move();
    }

    private void SetDirection(Vector3 dir)
    {
        agent.destination = dir;
        lookDir = dir;
        isWalking = true;
    }
}
