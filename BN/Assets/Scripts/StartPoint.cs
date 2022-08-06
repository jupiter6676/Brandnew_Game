using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;   // ���� �̵��Ǹ�, �÷��̾ ���۵� ��ġ
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
