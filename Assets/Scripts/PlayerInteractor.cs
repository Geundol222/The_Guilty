using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] float range;

    private Animator anim;
    private Vector3 targetDir;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        Collider[] colldiers = Physics.OverlapSphere(transform.position, range);
        foreach(Collider collider in colldiers)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            interactable?.Interact();
            targetDir = (collider.transform.position - transform.forward).normalized;
        }
    }

    private void OnInteract(InputValue value)
    {
        Interact();
    }

    public void Hide()
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
