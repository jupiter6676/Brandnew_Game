using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커스텀 클래스
// 커스텀 클래스를 인스펙터 창에서 수정하기 위해서 다음 구문을 추가
[System.Serializable]
public class Dialogue
{
    // 대사를 하는 캐릭터 이름을 inspector 창에 띄우기 위한 툴팁
    [Tooltip("대사 치는 캐릭터 이름")]
    public string name; // 캐릭터 이름

    [Tooltip("대사 내용")]
    public string[] contexts; // 배열이라 여러 대사를 담을 수 있음.
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // 대화 이벤트 이름
    public Vector2 line;    // x줄부터 y줄까지의 대사를 가져옴.
    public Dialogue[] dialogues;    // 대사를 여러 명이서 하기 때문에 배열 생성
}