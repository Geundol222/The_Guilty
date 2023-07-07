using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PaperCurl : MonoBehaviour
{
    [SerializeField] Transform front;
    [SerializeField] Transform mask;
    [SerializeField] Transform gradOutter;
    [SerializeField] Vector3 pos;

    void LateUpdate()
    {
        transform.position = pos;
        transform.eulerAngles = Vector3.zero;

        Vector3 position = front.localPosition;
        float theta = Mathf.Atan2(position.y, position.x) * 180.0f / Mathf.PI;

        if (theta <= 0.0f || theta >= 90.0f) return;

        float deg = -(90.0f - theta) * 2.0f;
        front.eulerAngles = new Vector3(0.0f, 0.0f, deg);

        mask.position = (transform.position + front.position) * 0.5f;
        mask.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f);

        gradOutter.position = mask.position;
        gradOutter.eulerAngles = new Vector3(0.0f, 0.0f, deg * 0.5f + 90.0f);

        transform.position = pos;
        transform.eulerAngles = Vector3.zero;
    }
}
