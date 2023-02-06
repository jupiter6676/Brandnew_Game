using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ŀ���� Ŭ����
// Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� ���� ������ �߰�
[System.Serializable]
public class Dialogue
{
    [Header("���ĵ� �̹���")]
    public Transform tf_standing;
    // public Image img_standing;

    [Tooltip("ĳ����")]   // ��縦 �ϴ� ĳ���� �̸��� inspector â�� ���� ���� ����
    public string name; // ĳ���� �̸�

    [HideInInspector]   // �ν����� â���� ������ �����.
    public string[] contexts; // �迭�̶� ���� ��縦 ���� �� �ִ�.

    [HideInInspector]
    public string[] spriteName; // ���� ���� ��������Ʈ �̹���
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // ��ȭ �̺�Ʈ �̸�
    public Vector2 line;    // x�ٺ��� y�ٱ����� ��縦 �����´�.
    public Dialogue[] dialogues;    // ��縦 ���� ���̼� �ϱ� ������ �迭 ����
}