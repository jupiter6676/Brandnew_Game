using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences;  // NPC의 대사를 담을 배열
    public Transform chatTransform; // 말풍선 생성 위치를 담을 변수
    public GameObject chatBoxPrefab;    // ChatBox를 복제하기 위해 GameObject 선언

    void Start()
    {
        
    }

    public void npcTalk()
    {
        GameObject go = Instantiate(chatBoxPrefab); // chatBoxPrefab을 복제

        // 복제한 ChatBox의 ChatSystem 스크립트의 Ondialogue를 호출
        // 인자에는 대시가 담긴 string 배열
        go.GetComponent<ChatSystem>().OnDialogue(sentences, chatTransform);
    }

    private void OnMouseDown()
    {
        npcTalk();
    }
}
