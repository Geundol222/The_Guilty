using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class PlayerRoomInteractor : MonoBehaviour
{
    [SerializeField] Transform itemPoint;
    [SerializeField] Transform doorPoint;
    [SerializeField] float range;
    [SerializeField] LayerMask itemMask;
    [SerializeField] LayerMask doorMask;

    private Collider itemCol;
    private GameObject doorObj;
    private OpenDoor door;
    private ItemInteractUI itemUI;
    private DoorInteractUI doorUI;
    private Collider col;
    private Animator anim;
    private RaycastHit hit;

    private bool isPick = false;
    private bool isPickable;

    private bool isOpen = false;
    private bool isOpenable;

    private void Awake()
    {
        col = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        InteractDoor();
        DoorInteractUIRenderer(isOpenable);
        InteractItem();
        IsPickable(isPickable);
    }

    private void OnInteract(InputValue value)
    {
        if (isPickable)
        {
            isPick = !isPick;
            PickItem();
        }
        else if (isOpenable)
        {
            OpenDoor();
        }
        else
            return;
    }

    private void InteractItem()
    {
        Collider[] colliders = Physics.OverlapSphere(itemPoint.position, range, itemMask);
        foreach (Collider collider in colliders)
        {
            if (collider != null && itemMask.IsContain(collider.gameObject.layer))
            {
                itemCol = collider;
                isPickable = true;
            }
            else
                isPickable = false;
        }
    }
    
    private void IsPickable(bool isPickable)
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

    private void PickItem()
    {
        if (isPick)
        {
            IInteractable interactable = itemCol.gameObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }

    private void OpenDoor()
    {
        if (isOpenable)
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            interactable?.Interact();
        }

    }

    private void InteractDoor()
    {
        if (Physics.Raycast(doorPoint.position, doorPoint.forward, out hit, 4f, doorMask))
        {
            if (doorMask.IsContain(hit.transform.gameObject.layer))
            {
                doorObj = hit.transform.gameObject;
                door = doorObj.GetComponent<OpenDoor>();
                isOpenable = true;
            }
        }
        else
            isOpenable = false;
    }

    private void DoorInteractUIRenderer(bool isOpenable)
    {
        if (isOpenable && !door.IsOpen)
        {
            if (doorUI == null || !doorUI.gameObject.activeSelf)
            {
                doorUI = GameManager.UI.ShowInGameUI<DoorInteractUI>("UI/InGameUI/DoorInteractUI");
                doorUI.SetTarget(transform);
                doorUI.SetOffSet(new Vector2(70, 50));
            }
            else
                return;

        }
        else
        {
            if (doorUI != null && doorUI.gameObject.activeSelf)
                GameManager.UI.CloseInGameUI(doorUI);
            else
                return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(itemPoint.position, range);
    }
}
