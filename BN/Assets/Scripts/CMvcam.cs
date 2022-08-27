using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMvcam : MonoBehaviour
{
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");

            if (tPlayer != null)
            {
                tFollowTarget = tPlayer.transform;
                vcam.LookAt = tFollowTarget;
                vcam.Follow = tFollowTarget;
            }
        }
    }
}