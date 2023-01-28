using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ŀ���� Ŭ����
// Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� ���� ������ �߰�
[System.Serializable]
public class Dialogue
{
    // ��縦 �ϴ� ĳ���� �̸��� inspector â�� ���� ���� ����
    [Tooltip("��� ġ�� ĳ���� �̸�")]
    public string name; // ĳ���� �̸�

    [Tooltip("��� ����")]
    public string[] contexts; // �迭�̶� ���� ��縦 ���� �� ����.
}

[System.Serializable]
public class DialogueEvent
{
    public string name;     // ��ȭ �̺�Ʈ �̸�
    public Vector2 line;    // x�ٺ��� y�ٱ����� ��縦 ������.
    public Dialogue[] dialogues;    // ��縦 ���� ���̼� �ϱ� ������ �迭 ����
}