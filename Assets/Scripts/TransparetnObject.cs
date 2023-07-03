using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparetnObject : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;

    private void OnTriggerEnter(Collider other)
    {
        if (obstacleMask.IsContain(other.gameObject.layer))
        {
            StartCoroutine(TransparentRoutine(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (obstacleMask.IsContain(other.gameObject.layer))
        {
            StartCoroutine(OpaqueRoutine(other));
        }
    }

    IEnumerator TransparentRoutine(Collider collider)
    {
        Renderer renderer = collider.gameObject.GetComponent<Renderer>();

        while (true)
        {
            StandardShaderUtils.ChangeRenderMode(renderer.material, StandardShaderUtils.BlendMode.Transparent);
            Color color = renderer.material.color;
            color.a = Mathf.Lerp(color.a, 0.1f, 0.1f);
            renderer.material.color = color;
            if (renderer.material.color.a <= 0.1f)
                yield break;

            yield return null;
        }
    }

    IEnumerator OpaqueRoutine(Collider collider)
    {
        Renderer renderer = collider.gameObject.GetComponent<Renderer>();

        while (true)
        {
            StandardShaderUtils.ChangeRenderMode(renderer.material, StandardShaderUtils.BlendMode.Opaque);
            Color color = renderer.material.color;
            color.a = Mathf.Lerp(color.a, 1f, 0.1f);
            renderer.material.color = color;

            if (renderer.material.color.a >= 1f)
                yield break;

            yield return null;
        }
    }
}
