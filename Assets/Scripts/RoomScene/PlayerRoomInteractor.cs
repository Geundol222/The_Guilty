using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;
using Color = UnityEngine.Color;

public class PlayerRoomInteractor : MonoBehaviour
{
    [SerializeField] Transform point;
    [SerializeField] float range;
    [SerializeField] LayerMask itemMask;
    [SerializeField] LayerMask doorMask;

    private ItemInteractUI itemUI;
    private DoorInteractUI doorUI;
    private Collider col;
    private Animator anim;
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
        OpenDoor();
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
            isOpen = !isOpen;
        }
        else
            return;
    }

    private void InteractItem()
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
            GameManager.UI.ShowPopUpUI<BookOpenPopUpUI>("UI/PopUpUI/BookOpenPopUpUI");
    }

    private void OpenDoor()
    {
        IInteractable interactable = GetComponent<IInteractable>();
        interactable?.Interact();
    }

    private void InteractDoor()
    {
        Collider[] colliders = Physics.OverlapSphere(point.position, range, doorMask);
        foreach (Collider collider in colliders)
        {
            if (collider != null && doorMask.IsContain(collider.gameObject.layer))
            {
                isOpenable = true;
            }
            else
                isOpenable = false;
        }
    }

    private void DoorInteractUIRenderer(bool isOpenable)
    {
        if (isOpenable)
        {
            if (doorUI == null || !doorUI.gameObject.activeSelf)
            {
                doorUI = GameManager.UI.ShowInGameUI<DoorInteractUI>("UI/InGameUI/DoorInteractUI");
                doorUI.SetTarget(transform);
                doorUI.SetOffSet(new Vector2(70, 50));
            }
            else if (doorUI != null && doorUI.gameObject.activeSelf)
            {
                if (!isOpen)
                    doorUI.Open();
                else
                    doorUI.Close();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(point.position, range);
    }
}
