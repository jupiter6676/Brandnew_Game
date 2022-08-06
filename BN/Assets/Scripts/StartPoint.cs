using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;   // 맵이 이동되면, 플레이어가 시작될 위치
    private PlayerMove thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMove>();

        if (startPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position = this.transform.position;
        }
    }

    
    void Update()
    {
        
    }
}
