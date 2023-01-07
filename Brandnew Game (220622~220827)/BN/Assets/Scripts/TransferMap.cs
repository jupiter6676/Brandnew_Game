using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  // �̵��� ���� �̸�

    private PlayerMove thePlayer;   // �÷��̾��� currentMapName ������ �����ϱ� ����

    void Start()
    {
        // FindObjectOfType<>: Hierachy�� �ִ� ��� ��ü�� <> ������Ʈ�� �˻� �� ���� (�ټ��� ��ü)
        // GetComponent<>: �ش� ��ũ��Ʈ�� ����� ��ü�� <> ������Ʈ�� �˻� �� ���� (���� ��ü)
        // �˻� ������ ���̷� �����ϸ� O
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // box collider�� ��� ���� ����Ǵ� ���� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // box collider�� ���� ������Ʈ�� �̸� ��ȯ
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }
}