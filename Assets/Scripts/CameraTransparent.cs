using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparent : MonoBehaviour
{
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] GameObject player;

    Renderer obstacleRenderer;
    List<GameObject> obstacleWall;
    Camera mainCam;

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
        RaycastHit hit;

        Vector3 rayDir;

        if (player != null)
        {
            rayDir = (player.transform.position - mainCam.transform.position).normalized;

            if (Physics.Raycast(mainCam.transform.position, rayDir, out hit, Vector3.Distance(mainCam.transform.position, player.transform.position), obstacleMask))
            {
                obstacleRenderer = hit.collider.gameObject.GetComponentInChildren<Renderer>();
                if (obstacleRenderer != null)
                {
                    obstacleWall.Add(obstacleRenderer.gameObject);
                    Material[] materials = obstacleRenderer.materials;
                    foreach (Material material in materials)
                    {
                        Color color = material.color;
                        color.a = 0.2f;
                        material.color = color;
                    }
                }
            }
            else
            {
                for (int i = 0; i < obstacleWall.Count; i++)
                {
                    obstacleRenderer = obstacleWall[i].GetComponentInChildren<Renderer>();
                    if (obstacleRenderer != null)
                    {
                        Material[] materials = obstacleRenderer.materials;
                        foreach (Material material in materials)
                        {
                            Color color = material.color;
                            color.a = 1f;
                            material.color = color;
                        }
                        obstacleWall.Remove(obstacleWall[i]);
                    }
                }
            }
        }
        else
            return;
    }
}
