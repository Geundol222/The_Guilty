using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class CameraTransparent : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject player;

    public List<GameObject> curWall;
    public List<GameObject> prevWall;
    Renderer[] renderers;
    Renderer[] returnRenders;
    Camera mainCam;
    RaycastHit[] hits;

    Coroutine fadeInRoutine;
    Coroutine fadeOutRoutine;

    private void Awake()
    {
        mainCam = Camera.main;
        curWall = new List<GameObject>();
        prevWall = new List<GameObject>();
    }

    private void LateUpdate()
    {
        ObstacleRay();
    }

    private void ObstacleRay()
    {
        if (player != null)
        {
            Vector3 rayDir = (player.transform.position - mainCam.transform.position).normalized;

            hits = Physics.RaycastAll(mainCam.transform.position, rayDir, Vector3.Distance(mainCam.transform.position, player.transform.position), obstacleMask);
            curWall.Clear();

            foreach (RaycastHit hit in hits)
            {
                curWall.Add(hit.collider.gameObject);
            }

            for (int i = 0; i < prevWall.Count; i++)
            {
                if (!curWall.Contains(prevWall[i]))
                {
                    // Enter
                    prevWall.Remove(prevWall[i]);
                    i--;
                }
            }

            for (int i = 0; i < curWall.Count; i++)
            {
                if (!prevWall.Contains(curWall[i]))
                {
                    // Exit
                    prevWall.Add(curWall[i]);                    
                }
            }
        }
        else
            return;
    }

    IEnumerator OpaqueRoutine()
    {
        for (int i = 0; i < prevWall.Count; i++)
        {
            Renderer[] renderers = prevWall[i].GetComponentsInChildren<Renderer>();
        }

        while (true)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                StandardShaderUtils.ChangeRenderMode(renderers[i].material, StandardShaderUtils.BlendMode.Opaque);
                Color color = renderers[i].material.color;
                color.a = Mathf.Lerp(color.a, 1f, 0.1f);
                renderers[i].material.color = color;
            }
            yield return null;
        }
    }

    //private void CheckList()
    //{
    //    if (prevWall.Count > 0)
    //    {
    //        int count = 0;
    //        for (int i = 0; i < prevWall.Count; i++)
    //            returnRenders = prevWall[i].GetComponentsInChildren<Renderer>();

    //        for (int i = 0; i < returnRenders.Length; i++)
    //        {
    //            if (returnRenders[i].material.color.a >= 1f)
    //                count++;
    //        }

    //        if (count == returnRenders.Length)
    //            prevWall.Clear();
    //    }
    //}

    //IEnumerator FadeInMaterial()
    //{
    //    for (int i = 0; i < prevWall.Count; i++)
    //    {
    //        renderers = prevWall[i].GetComponentsInChildren<Renderer>();
    //    }

    //    while (true)
    //    {
    //        for (int i = 0; i < renderers.Length; i++)
    //        {
    //            Color color = renderers[i].material.color;
    //            color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime * 0.5f);
    //            renderers[i].material.color = color;
    //            StandardShaderUtils.ChangeRenderMode(renderers[i].material, StandardShaderUtils.BlendMode.Opaque);
    //        }
    //        yield return null;
    //    }
    //}

    //IEnumerator FadeOutMaterial()
    //{
    //    for (int i = 0; i < curWall.Count; i++)
    //    {
    //        renderers = curWall[i].GetComponentsInChildren<Renderer>();
    //    }

    //    while (true)
    //    {
    //        for (int i = 0; i < renderers.Length; i++)
    //        {
    //            StandardShaderUtils.ChangeRenderMode(renderers[i].material, StandardShaderUtils.BlendMode.Transparent);
    //            Color color = renderers[i].material.color;
    //            color.a = Mathf.Lerp(color.a, 0.1f, Time.deltaTime * 0.5f);
    //            renderers[i].material.color = color;
    //        }
    //        yield return null;
    //    }
    //}
}
