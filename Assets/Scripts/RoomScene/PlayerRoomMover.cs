using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerRoomMover : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;

    private Animator anim;
    private Camera mainCam;
    private NavMeshAgent agent;
    private RaycastHit hit;

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
        if (Vector3.Distance(transform.position, hit.point) < 0.5f)
        {
            anim.SetFloat("WalkSpeed", 0f);
        }
    }

    private void OnMove(InputValue value)
    {
        if (Time.timeScale > 0f)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

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
        else
            return;
    }
}
