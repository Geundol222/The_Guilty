using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*if (hit.collider != null && !obstacleWall.Contains(hit.collider.gameObject))
            {
                obstacleRenderer = hit.collider.gameObject.GetComponentInChildren<Renderer>();
                if (obstacleRenderer != null)
                {
                    obstacleWall.Add(obstacleRenderer.gameObject);
                    Material[] materials = obstacleRenderer.materials;
                    foreach (Material material in materials)
                    {
                        wallMaterials.Add(material);
                    }
                    FadeMaterial(StandardShaderUtils.BlendMode.Transparent, 1f, 0.1f);
                }
            }
            else
            {
                for (int i = 0; i < obstacleWall.Count; i++)
                {
                    obstacleRenderer = obstacleWall[i].GetComponentInChildren<Renderer>();
                    if (obstacleRenderer != null)
                    {
                        FadeMaterial(StandardShaderUtils.BlendMode.Opaque, 0.1f, 1f);
                        Material[] materials = obstacleRenderer.materials;
                        foreach (Material material in materials)
                        {
                            wallMaterials.Remove(material);
                        }
                        obstacleWall.Remove(obstacleWall[i]);
                    }
                }
            }*/

public class CameraTransparent : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject player;

    Renderer obstacleRenderer;
    List<GameObject> obstacleWall;
    List<GameObject> hitList;
    List<Material> wallMaterials;
    Camera mainCam;
    RaycastHit[] hits;
    Vector3 rayDir;
    bool isContain;

    private void Awake()
    {
        mainCam = Camera.main;
        obstacleWall = new List<GameObject>();
        hitList = new List<GameObject>();
        wallMaterials = new List<Material>();
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
                if (hitList.Contains(hits[i].transform.gameObject))
                    continue;
                else
                    hitList.Add(hits[i].transform.gameObject);
            }

            for (int i = 0; i < hitList.Count; i++)
            {
                obstacleRenderer = hitList[i].GetComponentInChildren<Renderer>();
                Material[] addMaterials = obstacleRenderer.materials;

                foreach (Material material in addMaterials)
                    wallMaterials.Add(material);
                obstacleWall.Add(hitList[i]);

                FadeMaterial(StandardShaderUtils.BlendMode.Transparent, 1f, 0.1f);
            }

            CheckFade();
        }
        else
            return;
    }

    private void CheckFade()
    {
        if (hitList != null)
        {
            for (int i = 0; i < obstacleWall.Count; i++)
            {
                if (!obstacleWall.Contains(hitList[i]))
                {
                    obstacleRenderer = obstacleWall[i].GetComponentInChildren<Renderer>();
                    Material[] removeMaterials = obstacleRenderer.materials;

                    FadeMaterial(StandardShaderUtils.BlendMode.Opaque, 0.1f, 1f);

                    foreach (Material material in removeMaterials)
                        wallMaterials.Remove(material);
                    obstacleWall.Remove(obstacleWall[i]);
                }
            }

            for (int i = 0; i < hitList.Count; i++)
            {
                if (!hitList.Contains(obstacleWall[i]))
                {
                    obstacleRenderer = hitList[i].GetComponentInChildren<Renderer>();
                    Material[] addMaterials = obstacleRenderer.materials;

                    foreach (Material material in addMaterials)
                        wallMaterials.Add(material);
                    obstacleWall.Add(hitList[i]);

                    FadeMaterial(StandardShaderUtils.BlendMode.Transparent, 1f, 0.1f);
                }
            }
            hitList.Clear();
        }
    }

    private void FadeMaterial(StandardShaderUtils.BlendMode blendMode, float curAlpha, float changeAlpha)
    {
        for (int i = 0; i < wallMaterials.Count; i++)
        {
            StandardShaderUtils.ChangeRenderMode(wallMaterials[i], blendMode);
            Color color = wallMaterials[i].color;
            color.a = Mathf.Lerp(curAlpha, changeAlpha, Time.deltaTime * 0.5f);
            wallMaterials[i].color = color;
        }
    }
}
