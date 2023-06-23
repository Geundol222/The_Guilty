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
    [SerializeField] Transform point;
    [SerializeField] float range;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask itemMask;

    private Vector3 originPosition;
    private RaycastHit headHit;
    private RaycastHit legHit;
    private ItemInteractUI itemUI;
    private HideInteractUI hideUI;
    private NavMeshAgent agent;
    private Collider col;
    private Animator anim;
    private bool isLong;
    private bool isHidable;
    private bool isHide;
    private bool isPick = false;
    private bool isPickable;
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
        InteractWall();
        InteracUIRender(isHidable);

        InteractItem();
        IsPickable(isPickable);
    }

    public void InteractWall()
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
        {
            isInteract = !isInteract;
            Hide();
        }
        else if (isPickable)
        {
            isPick = !isPick;
            PickItem();
        }
        else
            return;
    }

    public void Hide()
    {
        if (isInteract)
        {
            anim.SetFloat("MoveSpeed", 0f);
            originPosition = transform.position;
            isHide = true;
            col.enabled = false;

            if (isLong)
            {
                anim.SetBool("IsStandHide", true);
                agent.destination = headHit.point;                
                StartCoroutine(DistanceCalculateRoutine(headHit));

            }
            else
            {
                anim.SetBool("IsCrouchHide", true);
                agent.destination = legHit.point;                
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

    public void InteractItem()
    {
        Collider[] colliders = Physics.OverlapSphere(point.position, range, itemMask);
        foreach (Collider collider in colliders)
        {
            if (collider != null && itemMask.IsContain(collider.gameObject.layer))
            {
                isPickable = true;
            }
            else
                isPickable = false;
        }
    }

    public void IsPickable(bool isPickable)
    {
        if (isPickable)
        {
            if (itemUI == null || !itemUI.gameObject.activeSelf)
            {
                itemUI = GameManager.UI.ShowInGameUI<ItemInteractUI>("UI/InGameUI/ItemInteractUI");
                itemUI.SetTarget(transform);
                itemUI.SetOffSet(new Vector2(70, 50));
            }
            else
                return;

        }
        else
        {
            if (itemUI != null && itemUI.gameObject.activeSelf)
                GameManager.UI.CloseInGameUI(itemUI);
            else
                return;
        }
    }

    public void PickItem()
    {
        if (isPick)
            GameManager.UI.ShowPopUpUI<BookOpenPopUpUI>("UI/PopUpUI/BookOpenPopUpUI");
    }
}
