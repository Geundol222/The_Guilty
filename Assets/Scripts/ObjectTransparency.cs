using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparency : MonoBehaviour
{
    public bool IsTransparent { get; private set; } = false;

    Renderer[] renderers;
    bool isReseting = false;
    float timer = 0f;

    Coroutine timeCheckCoroutine;
    Coroutine resetCoroutine;
    Coroutine becomeTransparentCoroutine;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void BecomeTransparent()
    {
        if (IsTransparent)
        {
            timer = 0f;
            return;
        }

        if (resetCoroutine != null && isReseting)
        {
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        SetMaterialTransparent();
        IsTransparent = true;
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
        SetMaterialOpaque();
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
    }

    IEnumerator ResetOriginalTransparentCoroutine()
    {
        IsTransparent = false;

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
