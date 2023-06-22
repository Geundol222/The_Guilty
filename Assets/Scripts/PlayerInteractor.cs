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

    private HideInteractUI hideUI;
    private NavMeshAgent agent;
    private Collider col;
    private Animator anim;
    private bool isLong;
    private bool isHidable;
    private bool isInteract = false;

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
        RaycastHit hit;

        if (Physics.Raycast(legChecker.position, legChecker.forward, out hit, 8f, obstacleMask))
        {
            isHidable = true;

            if (Physics.Raycast(headChecker.position, headChecker.forward, out hit, 8f, obstacleMask))
                isLong = true;
            else
                isLong = false;
        }
        else
            isHidable = false;

        Debug.DrawRay(headChecker.position, headChecker.forward * 8f, Color.red);
        Debug.DrawRay(legChecker.position, legChecker.forward * 8f, Color.red);
    }

    private void InteracUIRender(bool isHide)
    {
        if (isHide)
        {
            if (hideUI == null || !hideUI.gameObject.activeSelf)
            {
                hideUI = GameManager.UI.ShowInGameUI<HideInteractUI>("UI/InGameUI/HideInteractUI");
                hideUI.SetTarget(transform);
                hideUI.SetOffSet(new Vector2(70, 50));
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
            col.enabled = false;

            if (isLong)
            {
                Debug.Log("¼­¼­ ¼û±â");
                // TODO : ¼­¼­ ¼û´Â ¾Ö´Ï¸ÞÀÌ¼Ç
            }
            else
            {
                Debug.Log("¾É¾Æ¼­ ¼û±â");
                // TODO : ¾É¾Æ¼­ ¼û´Â ¾Ö´Ï¸ÞÀÌ¼Ç
            }
        }
        else
        {
            col.enabled = true;
        }
    }
}
