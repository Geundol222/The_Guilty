using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Transform headChecker;
    [SerializeField] Transform legChecker;
    [SerializeField] float range;
    [SerializeField] LayerMask obstacleMask;

    private Vector3 originPosition;
    private RaycastHit headHit;
    private RaycastHit legHit;
    private HideInteractUI hideUI;
    private NavMeshAgent agent;
    private Collider col;
    private Animator anim;
    private bool isLong;
    private bool isHidable;
    private bool isHide;
    private bool isInteract = false;

    public bool IsHide { get { return isHide; } }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InteractRay();

        InteracUIRender(isHidable);
    }

    public void InteractRay()
    {
        if (Physics.Raycast(legChecker.position, legChecker.forward, out legHit, 6f, obstacleMask))
        {
            isHidable = true;

            if (Physics.Raycast(headChecker.position, headChecker.forward, out headHit, 6f, obstacleMask))
                isLong = true;
            else
                isLong = false;
        }
        else if (isHide)
            isHidable = true;
        else
            isHidable = false;

        Debug.DrawRay(headChecker.position, headChecker.forward * 6f, Color.red);
        Debug.DrawRay(legChecker.position, legChecker.forward * 6f, Color.red);
    }

    private void InteracUIRender(bool isHidable)
    {
        if (isHidable)
        {
            if (hideUI == null || !hideUI.gameObject.activeSelf)
            {
                hideUI = GameManager.UI.ShowInGameUI<HideInteractUI>("UI/InGameUI/HideInteractUI");
                hideUI.SetTarget(transform);
                hideUI.SetOffSet(new Vector2(70, 50));
            }
            else if (hideUI != null && hideUI.gameObject.activeSelf)
            {
                if (!isInteract)
                    hideUI.Hide();
                else
                    hideUI.Escape();
            }
            else
                return;

        }
        else
        {
            if (hideUI != null && hideUI.gameObject.activeSelf)
                GameManager.UI.CloseInGameUI(hideUI);
            else
                return;
        }
    }

    private void OnInteract(InputValue value)
    {
        if (isHidable)
            isInteract = !isInteract;
        else
            return;

        Hide();
    }

    public void Hide()
    {
        if (isInteract)
        {
            originPosition = transform.position;
            isHide = true;
            col.enabled = false;

            if (isLong)
            {
                agent.destination = headHit.point;
                anim.SetBool("IsStandHide", true);
                StartCoroutine(DistanceCalculateRoutine(headHit));

            }
            else
            {
                agent.destination = legHit.point;
                anim.SetBool("IsCrouchHide", true);
                StartCoroutine(DistanceCalculateRoutine(legHit));
            }
        }
        else
        {
            StopAllCoroutines();

            isHide = false;
            col.enabled = true;
            agent.enabled = true;

            StartCoroutine(EscapeRoutine());
        }
    }

    IEnumerator EscapeRoutine()
    {
        if (isLong)
            anim.SetBool("IsStandHide", false);
        else
            anim.SetBool("IsCrouchHide", false);

        while (true)
        {
            agent.destination = originPosition;

            if (Vector3.Distance(transform.position, originPosition) < 0.3f)
                yield break;

            yield return null;
        }
        
    }

    IEnumerator DistanceCalculateRoutine(RaycastHit hit)
    {
        Vector3 lookDir = hit.collider.transform.forward.normalized;
        Quaternion lookRot = Quaternion.LookRotation(lookDir);
        transform.rotation = lookRot;

        while (true)
        {
            Vector3 playerPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 HitPosition = new Vector3(hit.point.x, 0, hit.point.z);

            if (Vector3.Distance(playerPosition, HitPosition) < 1f)
            {
                agent.enabled = false;
            }

            yield return null;
        }
    }
}
