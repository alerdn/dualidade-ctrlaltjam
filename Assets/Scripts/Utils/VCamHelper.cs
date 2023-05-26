using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamHelper : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;

    private void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _vcam.Follow = Player.Instance != null ? Player.Instance.transform : null;
    }
}
