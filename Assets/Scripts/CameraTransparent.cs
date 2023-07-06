using System.Collections.Generic;
using UnityEngine;

public class CameraTransparent : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private GameObject player;
    [SerializeField] private float fadeDuration = 1f;

    private List<Renderer> wallRenderers;
    private List<bool> isOpaque;
    private List<float> currentAlpha;
    private List<float> targetAlpha;
    private List<float> lerpTime;

    private void Awake()
    {
        wallRenderers = new List<Renderer>();
        isOpaque = new List<bool>();
        currentAlpha = new List<float>();
        targetAlpha = new List<float>();
        lerpTime = new List<float>();
    }

    private void Start()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (obstacleMask == (obstacleMask | (1 << renderer.gameObject.layer)))
            {
                wallRenderers.Add(renderer);
                isOpaque.Add(true);
                currentAlpha.Add(1f);
                targetAlpha.Add(1f);
                lerpTime.Add(0f);
            }
        }
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 rayDir = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDir, Vector3.Distance(transform.position, player.transform.position), obstacleMask);

        ResetWallTransparency();

        int wallCount = wallRenderers.Count;
        for (int i = 0; i < wallCount; i++)
        {
            if (hits.Length > 0)
            {
                if (isOpaque[i])
                {
                    isOpaque[i] = false;
                    targetAlpha[i] = 0f;
                    lerpTime[i] = 0f;
                }
            }
            else
            {
                if (!isOpaque[i])
                {
                    isOpaque[i] = true;
                    targetAlpha[i] = 1f;
                    lerpTime[i] = 0f;
                }
            }

            if (lerpTime[i] < fadeDuration)
            {
                lerpTime[i] += Time.deltaTime;
                currentAlpha[i] = Mathf.Lerp(currentAlpha[i], targetAlpha[i], lerpTime[i] / fadeDuration);
            }

            SetWallTransparency(i, currentAlpha[i]);
        }
    }

    private void ResetWallTransparency()
    {
        int wallCount = wallRenderers.Count;
        for (int i = 0; i < wallCount; i++)
        {
            if (isOpaque[i])
            {
                isOpaque[i] = false;
                targetAlpha[i] = 0f;
                lerpTime[i] = 0f;
                SetWallTransparency(i, 0f);
            }
        }
    }

    private void SetWallTransparency(int wallIndex, float alpha)
    {
        Renderer renderer = wallRenderers[wallIndex];
        foreach (Material material in renderer.materials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;

            if (alpha < 1f)
            {
                StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Transparent);
            }
            else
            {
                StandardShaderUtils.ChangeRenderMode(material, StandardShaderUtils.BlendMode.Opaque);
            }
        }
    }

}
