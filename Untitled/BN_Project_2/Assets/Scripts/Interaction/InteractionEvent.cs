using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    // DatabaseManager�� ����� ���� ��� �����͸� �����´�.
    public Dialogue[] GetDialogue()
    {
        dialogue.dialogues = DatabaseManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);

        return dialogue.dialogues;
    }
}
