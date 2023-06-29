using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableItem : MonoBehaviour, IDragHandler, IScrollHandler
{
    [SerializeField] GameObject item;

    float maxZoom = 3f;
    Vector3 originPosition;
    Vector3 objectScale;
    bool isZoomed = false;

    private void Awake()
    {
        originPosition = item.transform.position;
        objectScale = item.transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.delta.x * Time.unscaledDeltaTime;
        float y = eventData.delta.y * Time.unscaledDeltaTime;

        if (!isZoomed)
        {
            Vector3 eulerAngle = new Vector3(0, -x * 10f, y * 10f);

            Quaternion rot = item.transform.rotation;
            item.transform.rotation = Quaternion.Euler(eulerAngle) * rot;
        }
        else
        {
            item.transform.position += new Vector3(x * 0.5f, y * 0.5f, 0);
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        Vector3 delta = Vector3.one * (eventData.scrollDelta.y * Time.unscaledDeltaTime * 10f);
        Vector3 Scale = item.transform.localScale + delta;

        Scale = ClampDesiredScale(Scale);

        item.transform.localScale = Scale;

        if (Scale.magnitude > objectScale.magnitude)
        {
            isZoomed = true;
        }
        else
        {
            isZoomed = false;
            item.transform.position = originPosition;
        }
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(objectScale, desiredScale);
        desiredScale = Vector3.Min(objectScale * maxZoom, desiredScale);

        return desiredScale;
    }
}
