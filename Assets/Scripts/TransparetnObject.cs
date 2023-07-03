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
            StartCoroutine
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    IEnumerator TransparentRoutine(Collider collider)
    {
        Renderer[] renderers = collider.gameObject.GetComponentsInChildren<Renderer>();

        while (true)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                StandardShaderUtils.ChangeRenderMode(renderers[i].material, StandardShaderUtils.BlendMode.Transparent);
                Color color = renderers[i].material.color;
                color.a = Mathf.Lerp(color.a, 0.1f, Time.deltaTime * 0.5f);
                renderers[i].material.color = color;
            }
            yield return null;
        }
    }
}
