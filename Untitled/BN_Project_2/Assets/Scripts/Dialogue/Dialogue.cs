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

// Ŀ���� Ŭ����
[System.Serializable]   // Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� �� ������ �߰�
public class Dialogue
{
    [Header("ī�޶� Ÿ���� ���")]
    public CameraType cameraType;

    [Header("���ĵ� �̹���")]
    public Transform tf_standing;

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
public class DialogueEvent
{
    public string name;     // ��ȭ �̺�Ʈ �̸�
    public Vector2 line;    // x�ٺ��� y�ٱ����� ��縦 �����´�.
    public Dialogue[] dialogues;    // ��縦 ���� ���̼� �ϱ� ������ �迭 ����
}