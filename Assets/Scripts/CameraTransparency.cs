using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparency : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject player;

    List<GameObject> obstacleWall;
    Camera mainCam;
    RaycastHit[] hits;
    Vector3 rayDir;

    Renderer[] renderers;
    bool isReseting = false;
    bool isTransparent = false;
    float timer = 0f;

    Coroutine timeCheckCoroutine;
    Coroutine resetCoroutine;
    Coroutine becomeTransparentCoroutine;

    private void Awake()
    {
        mainCam = Camera.main;
        obstacleWall = new List<GameObject>();
    }

    private void LateUpdate()
    {
        ObstacleTransparency();
    }

    private void ObstacleTransparency()
    {
        if (player != null)
        {
            rayDir = (player.transform.position - mainCam.transform.position).normalized;

            hits = Physics.RaycastAll(mainCam.transform.position, rayDir, Vector3.Distance(mainCam.transform.position, player.transform.position), obstacleMask);

            for (int i = 0; i < hits.Length; i++)
            {
                renderers = hits[i].transform.GetComponentsInChildren<Renderer>();

                obstacleWall.Add(hits[i].transform.gameObject);

                for (int j = 0; j < obstacleWall.Count; j++)
                {
                    BecomeTransparent();
                }
            }
        }
        else
            return;
    }

    public void BecomeTransparent()
    {
        if (isTransparent)
        {
            timer = 0f;
            return;
        }

        if (resetCoroutine != null && isReseting)
        {
            isReseting = false;
            isTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        SetMaterialTransparent();
        isTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCoroutine());
    }

    IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a > 0.1f)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a -= Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                CheckTimer();
                break;
            }

            yield return null;
        }
    }

    private void SetMaterialTransparent()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Transparent);
            }
        }
    }

    private void SetMaterialOpaque()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Opaque);
            }
        }
    }

    public void ResetOriginalTransparent()
    {
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
        SetMaterialOpaque();
    }

    IEnumerator ResetOriginalTransparentCoroutine()
    {
        isTransparent = false;

        while (true)
        {
            bool isComplete = true;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a < 1f)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a += Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                isReseting = false;
                break;
            }

            yield return null;
        }
    }

    public void CheckTimer()
    {
        if (timeCheckCoroutine != null)
            StopCoroutine(timeCheckCoroutine);
        timeCheckCoroutine = StartCoroutine(CheckTimerCoroutine());
    }

    IEnumerator CheckTimerCoroutine()
    {
        timer = 0f;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > 0.5f)
            {
                isReseting = true;
                ResetOriginalTransparent();
                break;
            }

            yield return null;
        }
    }
}
