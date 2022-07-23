using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ParallaxBackground : MonoBehaviour
{
    // ParallaxCamera 인스턴스 생성
    public ParallaxCamera parallaxCamera;
    
    // 배경 오브젝트의 자식 = Parallax Scroll을 적용할 배경 요소들
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();    // 시작 시 메인 카메라를 비춤
        
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;   // delegate 호출
       
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                // layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }
    
    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
