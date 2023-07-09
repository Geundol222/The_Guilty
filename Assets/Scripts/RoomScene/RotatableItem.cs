using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableItem : MonoBehaviour, IDragHandler, IScrollHandler
{
    [SerializeField] Transform startPosition;
    [SerializeField] GameObject realItem;
    [SerializeField] TMP_Text rotateText;

    float maxZoom = 3f;
    Vector3 objectScale;
    bool isZoomed = false;

    private void Awake()
    {
        objectScale = realItem.transform.localScale;
        rotateText.text = "Rotate";
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x = eventData.delta.x * Time.unscaledDeltaTime;
        float y = eventData.delta.y * Time.unscaledDeltaTime;

        if (!isZoomed)
        {
            Vector3 eulerAngle = new Vector3(y * 10f, -x * 10f, 0);

            Quaternion rot = realItem.transform.rotation;
            realItem.transform.rotation = Quaternion.Euler(eulerAngle) * rot;
        }
        else
        {
            realItem.transform.position += new Vector3(x * 0.5f, y * 0.5f, 0);
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        Vector3 delta = Vector3.one * (eventData.scrollDelta.y * Time.unscaledDeltaTime * 10f);
        Vector3 Scale = realItem.transform.localScale + delta;

        Scale = ClampDesiredScale(Scale);

        realItem.transform.localScale = Scale;

        if (Scale.magnitude > objectScale.magnitude)
        {
            isZoomed = true;
            rotateText.text = "Move";
        }
        else
        {
            isZoomed = false;
            rotateText.text = "Rotate";
            realItem.transform.position = startPosition.position;
            realItem.transform.rotation = startPosition.rotation;
        }
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale)
    {
        desiredScale = Vector3.Max(objectScale, desiredScale);
        desiredScale = Vector3.Min(objectScale * maxZoom, desiredScale);

        return desiredScale;
    }
}
