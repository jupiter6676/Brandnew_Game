using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcChatText : MonoBehaviour
{
    public string[] sentences;  // NPC�� ��縦 ���� �迭
    public Transform chatTransform; // ��ǳ�� ���� ��ġ�� ���� ����
    public GameObject chatBoxPrefab;    // ChatBox�� �����ϱ� ���� GameObject ����

    void Start()
    {
        
    }

    public void npcTalk()
    {
        GameObject go = Instantiate(chatBoxPrefab); // chatBoxPrefab�� ����

        // ������ ChatBox�� ChatSystem ��ũ��Ʈ�� Ondialogue�� ȣ��
        go.GetComponent<ChatSystem>().OnDialogue(sentences);
    }
}
