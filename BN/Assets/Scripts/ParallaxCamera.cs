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
        oldPosition = transform.position.x; // ���� �� oldPosition = ���� x ��ǥ

        DontDestroyOnLoad(this.gameObject);
    }
    
    void FixedUpdate()
    {
        if (transform.position.x != oldPosition)    // �÷��̾ �̵�
        {
            if (onCameraTranslate != null)
            {
                // delta = ���� ��ġ - ���� ��ġ
                float delta = oldPosition - transform.position.x;
                onCameraTranslate(delta);   // delta��ŭ ī�޶� �̵�
            }

            oldPosition = transform.position.x; // oldPosition�� ���� ��ġ�� �ʱ�ȭ
        }
    }
}
