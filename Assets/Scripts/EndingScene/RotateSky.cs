using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    [SerializeField] Vector3 lookDir;
    [SerializeField] float rotateSpeed;

    private void Start()
    {
        transform.Rotate(lookDir);
    }

    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
    }
}
