using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
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
        // ���ڿ��� ��ð� ��� string �迭
        go.GetComponent<ChatSystem>().OnDialogue(sentences, chatTransform);
    }

    private void OnMouseDown()
    {
        npcTalk();
    }
}
