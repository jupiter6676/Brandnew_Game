using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CameraType
{
    Default, // �ƹ� ȿ�� X
    FadeOut,
    FadeIn,
    FlashOut,
    FlashIn,
    ShowCutscene,
    HideCutscene,
}

public enum AppearType
{
    Default,    // �ƹ� ��ȭ X
    Change,     // Appear Ȥ�� Disappear
}


// Ŀ���� Ŭ����
[System.Serializable]   // Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� �� ������ �߰�
public class Dialogue
{
    [Header("ī�޶� Ÿ���� ���")]
    public CameraType cameraType;

    //[Header("���ĵ� �̹���")]
    //public Transform tf_standing;

    [Tooltip("ĳ����")]    // ��縦 �ϴ� ĳ���� �̸��� inspector â�� ���� ���� ����
    public string name;    // ĳ���� �̸�

    [HideInInspector]         // �ν����� â���� ������ �����.
    public string[] contexts; // �迭�̶� ���� ��縦 ���� �� �ִ�.

    [HideInInspector]
    public string[] spriteName;     // ���� ���� ���ĵ� �̹���

    [HideInInspector]
    public string[] cutsceneName;   // ���� ���� �ƽ� �̹���
}

[System.Serializable]
public class EventTiming
{
    public int eventNum;            // ���� �̺�Ʈ�� ��ȣ
    public int[] eventConditions;   // ������ �Ǵ� �̺�Ʈ���� ��ȣ ����Ʈ
    public bool conditionFlag;      // true: eventConditions �̺�Ʈ�� ���� �� ���� / false: �� ���� �� ����
    public int eventEndNum;         // �� ��ȣ�� �̺�Ʈ�� ���� ������ ����
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // ��ȭ �̺�Ʈ �̸�
    public EventTiming eventTiming;

    // ��ȣ�ۿ� �� ���
    public Vector2 line;    // x�ٺ��� y�ٱ����� ��縦 �����´�.
    public Dialogue[] dialogues;    // ��縦 ���� ���̼� �ϱ� ������ �迭 ����

    // ��ȣ�ۿ� �� �޶��� ���
    [Space]
    public Vector2 lineAfter;
    public Dialogue[] dialoguesAfter;

    [Space]
    public AppearType appearType;
    public GameObject[] go_appearTargets;       // �̺�Ʈ �� ���� �����ų ������Ʈ �迭
    public GameObject[] go_disappearTargets;    // �̺�Ʈ �� �����ų ������Ʈ �迭

    [Space]
    public GameObject go_nextEvent; // null �� ��ȭ �� / null X �� ���� �̺�Ʈ ���� ȣ��
}