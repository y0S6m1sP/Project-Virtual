using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFindPlayer : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        virtualCamera.Follow = PlayerManager.instance.player.transform;
    }
}
