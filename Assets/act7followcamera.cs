using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class act7followcamera : MonoBehaviour
{

    private CinemachineVirtualCamera camera;
    [SerializeField] private Transform gate;

    private void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void FollowTheGate()
    {
        camera.Follow = gate;
    }

    public void FollowBackPlayer()
    {
        camera.Follow = GameObject.FindWithTag("Player").transform;
    }



}
