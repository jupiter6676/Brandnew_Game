using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CameraType
{
    Default, // 아무 효과 X
    FadeOut,
    FadeIn,
    FlashOut,
    FlashIn,
    ShowCutscene,
    HideCutscene,
}

public enum AppearType
{
    Default,    // 아무 변화 X
    Change,     // Appear 혹은 Disappear
}


// 커스텀 클래스
[System.Serializable]   // 커스텀 클래스를 인스펙터 창에서 수정하기 위해서 이 구문을 추가
public class Dialogue
{
    [Header("카메라 타겟팅 대상")]
    public CameraType cameraType;

    //[Header("스탠딩 이미지")]
    //public Transform tf_standing;

    [Tooltip("캐릭터")]    // 대사를 하는 캐릭터 이름을 inspector 창에 띄우기 위한 툴팁
    public string name;    // 캐릭터 이름

    [HideInInspector]         // 인스펙터 창에서 변수를 숨긴다.
    public string[] contexts; // 배열이라 여러 대사를 담을 수 있다.

    [HideInInspector]
    public string[] spriteName;     // 여러 개의 스탠딩 이미지

    [HideInInspector]
    public string[] cutsceneName;   // 여러 개의 컷신 이미지
}

[System.Serializable]
public class EventTiming
{
    public int eventNum;            // 현재 이벤트의 번호
    public int[] eventConditions;   // 조건이 되는 이벤트들의 번호 리스트
    public bool conditionFlag;      // true: eventConditions 이벤트를 봤을 때 등장 / false: 안 봤을 때 등장
    public int eventEndNum;         // 이 번호의 이벤트를 보면 무조건 퇴장
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // 대화 이벤트 이름
    public EventTiming eventTiming;

    // 상호작용 전 대사
    public Vector2 line;    // x줄부터 y줄까지의 대사를 가져온다.
    public Dialogue[] dialogues;    // 대사를 여러 명이서 하기 때문에 배열 생성

    // 상호작용 후 달라진 대사
    [Space]
    public Vector2 lineAfter;
    public Dialogue[] dialoguesAfter;

    [Space]
    public AppearType appearType;
    public GameObject[] go_appearTargets;       // 이벤트 후 새로 등장시킬 오브젝트 배열
    public GameObject[] go_disappearTargets;    // 이벤트 후 퇴장시킬 오브젝트 배열

    [Space]
    public GameObject go_nextEvent; // null → 대화 끝 / null X → 다음 이벤트 연속 호출
}