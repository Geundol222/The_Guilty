using System.Collections.Generic;
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
    private RaycastHit hit;

    private bool isPick = false;

    private bool isOpenable;

    public bool IsWatch { get; private set; } = false;

    private void Update()
    {
        InteractDoor();
        DoorInteractUIRenderer(isOpenable);
        IsPickable(InteractItem());
    }

    private void OnInteract(InputValue value)
    {
        if (Time.timeScale > 0f)
        {
            if (InteractItem() && itemCol != null)
            {
                if (itemMask.IsContain(itemCol.gameObject.layer))
                {
                    if (itemCol.gameObject.name == "Paper")
                    {
                        IsWatch = !IsWatch;
                        WatchItem();
                    }
                    else
                    {
                        isPick = !isPick;
                        PickItem();
                    }
                }
            }
            else if (isOpenable)
            {
                OpenDoor();
            }
            else
                return;
        }
        else
            return;
    }

    private bool InteractItem()
    {
        Collider[] colliders = Physics.OverlapSphere(itemPoint.position, range, itemMask);
        foreach (Collider collider in colliders)
        {
            if (collider != null && itemMask.IsContain(collider.gameObject.layer))
            {
                itemCol = collider;
                return true;
            }
            else if (Vector3.Distance(transform.position, collider.gameObject.transform.position) > range)
            {
                return false;
            }
        }
        return false;
    }

    private void IsPickable(bool isPickable)
    {
        if (isPickable)
        {
            if (itemUI == null || !itemUI.gameObject.activeSelf)
            {
                if (itemCol.gameObject.name == "Paper")
                {
                    itemUI = GameManager.UI.ShowInGameUI<ItemInteractUI>("UI/InGameUI/ItemInteractUI");
                    itemUI.WatchText();
                }
                else
                {
                    itemUI = GameManager.UI.ShowInGameUI<ItemInteractUI>("UI/InGameUI/ItemInteractUI");
                    itemUI.ItemText(); 
                }
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

    private void WatchItem()
    {
        IInteractable interactable = itemCol.gameObject.GetComponent<IInteractable>();
        interactable?.Interact();
    }

    private void PickItem()
    {
        if (isPick)
        {
            isPick = false;
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

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(itemPoint.position, range);
    // }
}
