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

    List<GameObject> obstacleWall;
    Camera mainCam;
    RaycastHit[] hits;
    Vector3 rayDir;

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

                for (int j = 0; j < obstacleWall.Count; j++)
                {

                }
            }
        }
        else
            return;
    }

    //private void CheckFade()
    //{
    //    if (hitList != null)
    //    {
    //        for (int i = 0; i < hitList.Count; i++)
    //        {
    //            if (!obstacleWall.Contains(hitList[i]))
    //            {
    //                foreach (GameObject obj in obstacleWall)
    //                    obstacleRenderer = obj.GetComponentInChildren<Renderer>();
    //                Material[] removeMaterials = obstacleRenderer.materials;

    //                FadeMaterial(StandardShaderUtils.BlendMode.Opaque, 1f);

    //                foreach (Material material in removeMaterials)
    //                    wallMaterials.Remove(material);
    //                obstacleWall.Remove(obstacleWall[i]);
    //            }
    //        }

    //        for (int i = 0; i < obstacleWall.Count; i++)
    //        {
    //            if (!hitList.Contains(obstacleWall[i]))
    //            {
    //                foreach (GameObject obj in hitList)
    //                    obstacleRenderer = obj.GetComponentInChildren<Renderer>();
    //                Material[] addMaterials = obstacleRenderer.materials;

    //                foreach (Material material in addMaterials)
    //                {
    //                    if (wallMaterials.Contains(material))
    //                        continue;
    //                    else
    //                        wallMaterials.Add(material);
    //                }

    //                obstacleWall.Add(hitList[i]);
    //                obstacleWall.Distinct();

    //                FadeMaterial(StandardShaderUtils.BlendMode.Transparent, 0.1f);
    //            }
    //            hitList.Clear();
    //        }
    //    }
    //}

    // private void FadeMaterial(StandardShaderUtils.BlendMode blendMode, float changeAlpha)
    // {
    //     for (int i = 0; i < wallMaterials.Count; i++)
    //     {
    //         StandardShaderUtils.ChangeRenderMode(wallMaterials[i], blendMode);
    //         Color color = wallMaterials[i].color;
    //         color.a = Mathf.Lerp(color.a, changeAlpha, Time.deltaTime * 0.5f);
    //         wallMaterials[i].color = color;
    //     }
    // }
}
