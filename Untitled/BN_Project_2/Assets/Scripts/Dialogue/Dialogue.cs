using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 커스텀 클래스
// 커스텀 클래스를 인스펙터 창에서 수정하기 위해서 다음 구문을 추가
[System.Serializable]
public class Dialogue
{
    [Header("스탠딩 이미지")]
    public Transform tf_standing;
    // public Image img_standing;

    [Tooltip("캐릭터")]   // 대사를 하는 캐릭터 이름을 inspector 창에 띄우기 위한 툴팁
    public string name; // 캐릭터 이름

    [HideInInspector]   // 인스펙터 창에서 변수를 숨긴다.
    public string[] contexts; // 배열이라 여러 대사를 담을 수 있다.

    [HideInInspector]
    public string[] spriteName; // 여러 개의 스프라이트 이미지
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // 대화 이벤트 이름
    public Vector2 line;    // x줄부터 y줄까지의 대사를 가져온다.
    public Dialogue[] dialogues;    // 대사를 여러 명이서 하기 때문에 배열 생성
}