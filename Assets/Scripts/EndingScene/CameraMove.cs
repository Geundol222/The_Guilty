using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMove : MonoBehaviour
{
    [SerializeField] CinemachineDollyCart cart;
    [SerializeField] CinemachineSmoothPath path;
    [SerializeField] CinemachineVirtualCamera dollyCam;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private CinemachineTrackedDolly dolly;
    private float lerpTime = 0;
    private float duration = 2f;

    private void Awake()
    {
        dolly = dollyCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void LateUpdate()
    {
        if (cart.m_Position > path.PathLength - 60)
        {
            ChangeCamera();
        }
    }

    public void MoveStart()
    {
        dolly.m_AutoDolly.m_Enabled = true;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(0.5f);

        while (lerpTime < duration)
        {
            lerpTime += Time.deltaTime * 0.3f;
            cart.m_Speed = Mathf.Lerp(0, 80, lerpTime / duration);

            if (cart.m_Speed >= 80)
                break;

            yield return null;
        }
    }

    private void ChangeCamera()
    {
        dollyCam.Priority = 1;
        virtualCamera.Priority = 10;
    }
}
