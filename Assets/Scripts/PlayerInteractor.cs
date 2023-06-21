using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] LayerMask obstacleMask;

    private Animator anim;
    private Vector3 targetDir;
    private bool isInteract = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        Collider[] colldiers = Physics.OverlapSphere(transform.position, range, obstacleMask);
        foreach(Collider collider in colldiers)
        {
            HideInteractUI hideUI = GameManager.UI.ShowInGameUI<HideInteractUI>("UI/InGameUI/HideInteractUI");
            hideUI.SetTarget(transform);
            hideUI.SetOffSet(new Vector2(70, 50));
        }
    }

    private void OnInteract(InputValue value)
    {
        isInteract = !isInteract;

        Interact();
    }

    public void Hide(GameObject obj)
    {
        Quaternion lookDir = Quaternion.LookRotation(transform.forward, targetDir);
        transform.rotation = lookDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
