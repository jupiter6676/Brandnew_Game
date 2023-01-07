using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class ParallaxBackground : MonoBehaviour
{
    // ParallaxCamera �ν��Ͻ� ����
    public ParallaxCamera parallaxCamera;
    
    // ��� ������Ʈ�� �ڽ� = Parallax Scroll�� ������ ��� ��ҵ�
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();    // ���� �� ���� ī�޶� ����
        
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;   // delegate ȣ��
       
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
