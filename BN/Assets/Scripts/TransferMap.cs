using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  // �̵��� ���� �̸�

    void Start()
    {
        
    }

    // box collider�� ��� ���� ����Ǵ� ���� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // box collider�� ���� ������Ʈ�� �̸� ��ȯ
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(transferMapName);
        }
    }
}
