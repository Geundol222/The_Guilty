using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPopUpUI : PopUpUI, IDragHandler
{
    [SerializeField] GameObject itemPosition;

    GameObject uiObj;
    ParticleSystem itemParticle;
    Vector3 rotateDir;

    protected override void Awake()
    {
        base.Awake();

        buttons["Blocker"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI<ItemPopUpUI>(); });
    }

    public void ShowItem(GameObject obj)
    {
        uiObj = GameManager.Resource.Instantiate<GameObject>("Prefabs/RoomScene/Knife", itemPosition.transform.position, itemPosition.transform.rotation, true);
        itemParticle = uiObj.GetComponentInChildren<ParticleSystem>();
        GameManager.Resource.Destroy(itemParticle.gameObject);
        uiObj.transform.parent = transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 eulerAngle = new Vector3(0, -eventData.delta.x * Time.unscaledDeltaTime * 10f, eventData.delta.y * Time.unscaledDeltaTime * 10f);

        Quaternion rot = uiObj.transform.rotation;
        uiObj.transform.rotation = Quaternion.Euler(eulerAngle) * rot;
    }
}
