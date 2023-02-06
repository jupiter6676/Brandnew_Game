using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    // DatabaseManager�� ����� ���� ��� �����͸� �����´�.
    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_dialogueEvent = new DialogueEvent();    // �ӽ� ����
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            // dialogueEvent�� ���� Standing Image ������Ʈ�� �ӽ� ������ �ֱ�
            t_dialogueEvent.dialogues[i].tf_standing = dialogueEvent.dialogues[i].tf_standing;
        }

        // ������ �ӽ� ���� �����
        dialogueEvent.dialogues = t_dialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }
}
