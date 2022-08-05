using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    
    void Start()
    {
        oldPosition = transform.position.x; // 시작 시 oldPosition = 현재 x 좌표

        DontDestroyOnLoad(this.gameObject);
    }
    
    void FixedUpdate()
    {
        if (transform.position.x != oldPosition)    // 플레이어가 이동
        {
            if (onCameraTranslate != null)
            {
                // delta = 이전 위치 - 현재 위치
                float delta = oldPosition - transform.position.x;
                onCameraTranslate(delta);   // delta만큼 카메라 이동
            }

            oldPosition = transform.position.x; // oldPosition을 현재 위치로 초기화
        }
    }
}
