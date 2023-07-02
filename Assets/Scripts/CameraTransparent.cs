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

    List<GameObject> obstacleWall;
    List<GameObject> returnWall;
    Renderer[] renderers;
    Renderer[] returnRenders;
    Camera mainCam;
    RaycastHit[] hits;

    Coroutine fadeInRoutine;
    Coroutine fadeOutRoutine;

    private void Awake()
    {
        mainCam = Camera.main;
        obstacleWall = new List<GameObject>();
        returnWall = new List<GameObject>();
    }

    private void LateUpdate()
    {
        ObstacleRay();
        CheckList();
    }

    private void ObstacleRay()
    {
        if (player != null)
        {
            Vector3 rayDir = (player.transform.position - mainCam.transform.position).normalized;

            hits = Physics.RaycastAll(mainCam.transform.position, rayDir, Vector3.Distance(mainCam.transform.position, player.transform.position), obstacleMask);

            if (obstacleWall.Count > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!obstacleWall.Contains(hits[i].transform.gameObject))
                    {
                        obstacleWall.Add(hits[i].transform.gameObject);
                    }
                }

                for (int i = 0; i < obstacleWall.Count; i++)
                {
                    returnWall.Add(obstacleWall[i]);
                    obstacleWall.Remove(obstacleWall[i]);
                }
            }
            else
            {
                foreach (RaycastHit hit in hits)
                {
                    if (obstacleWall.Contains(hit.transform.gameObject))
                        continue;
                    else
                        obstacleWall.Add(hit.transform.gameObject);
                }
            }
        }
        else
            return;
    }

    private void CheckList()
    {
        if (returnWall.Count > 0)
        {
            int count = 0;
            for (int i = 0; i < returnWall.Count; i++)
                returnRenders = returnWall[i].GetComponentsInChildren<Renderer>();

            for (int i = 0; i < returnRenders.Length; i++)
            {
                if (returnRenders[i].material.color.a >= 1f)
                    count++;
            }

            if (count == returnRenders.Length)
                returnWall.Clear();
        }
    }

    IEnumerator FadeInMaterial()
    {
        for (int i = 0; i < returnWall.Count; i++)
        {
            renderers = returnWall[i].GetComponentsInChildren<Renderer>();
        }

        while (true)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Color color = renderers[i].material.color;
                color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime * 0.5f);
                renderers[i].material.color = color;
                StandardShaderUtils.ChangeRenderMode(renderers[i].material, StandardShaderUtils.BlendMode.Opaque);
            }
            yield return null;
        }
    }

    IEnumerator FadeOutMaterial()
    {
        for (int i = 0; i < obstacleWall.Count; i++)
        {
            renderers = obstacleWall[i].GetComponentsInChildren<Renderer>();
        }

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
