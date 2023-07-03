using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparetnObject : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private GameObject player;
    [SerializeField] private float fadeDuration = 1f;

    private List<Renderer> wallRenderers;
    private Dictionary<Renderer, float> initialTransparency;
    private Dictionary<Renderer, bool> isOpaque;
    private Dictionary<Renderer, float> targetAlpha;
    private Dictionary<Renderer, float> lerpTime;

    private void Awake()
    {
        wallRenderers = new List<Renderer>();
        initialTransparency = new Dictionary<Renderer, float>();
        isOpaque = new Dictionary<Renderer, bool>();
        targetAlpha = new Dictionary<Renderer, float>();
        lerpTime = new Dictionary<Renderer, float>();
    }

    private void Start()
    {
        // Obtain all wall renderers in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (obstacleMask == (obstacleMask | (1 << renderer.gameObject.layer)))
            {
                wallRenderers.Add(renderer);
                initialTransparency[renderer] = GetMaxAlpha(renderer);
                isOpaque[renderer] = true;
                targetAlpha[renderer] = 1f;
                lerpTime[renderer] = 0f;
            }
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        Vector3 rayDir = (player.transform.position - transform.position).normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, rayDir, Vector3.Distance(transform.position, player.transform.position), obstacleMask);

        foreach (Renderer renderer in wallRenderers)
        {
            if (hits.Length > 0)
            {
                if (isOpaque[renderer])
                {
                    isOpaque[renderer] = false;
                    targetAlpha[renderer] = 0f;
                    lerpTime[renderer] = 0f;
                }
            }
            else
            {
                if (!isOpaque[renderer])
                {
                    isOpaque[renderer] = true;
                    targetAlpha[renderer] = initialTransparency[renderer];
                    lerpTime[renderer] = 0f;
                }
            }

            if (lerpTime[renderer] < fadeDuration)
            {
                lerpTime[renderer] += Time.deltaTime;
                float currentAlpha = Mathf.Lerp(initialTransparency[renderer], targetAlpha[renderer], lerpTime[renderer] / fadeDuration);
                SetWallTransparency(renderer, currentAlpha);
            }
        }
    }

    private float GetMaxAlpha(Renderer renderer)
    {
        float maxAlpha = 0f;
        foreach (Material material in renderer.materials)
        {
            maxAlpha = Mathf.Max(maxAlpha, material.color.a);
        }
        return maxAlpha;
    }

    private void SetWallTransparency(Renderer renderer, float alpha)
    {
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
