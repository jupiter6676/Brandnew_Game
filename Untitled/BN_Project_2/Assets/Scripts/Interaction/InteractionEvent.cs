using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;  // true �� �ڵ����� ����Ǵ� �̺�Ʈ

    [SerializeField] DialogueEvent dialogueEvent;

    DialogueManager theDM;

    // DatabaseManager�� ����� ���� ��� �����͸� �����´�.
    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_dialogueEvent = new DialogueEvent();    // �ӽ� ����
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            // dialogueEvent�� ���� Standing Image ������Ʈ�� �ӽ� ������ �ֱ�
            t_dialogueEvent.dialogues[i].tf_standing = dialogueEvent.dialogues[i].tf_standing;

            // dialogueEvent�� ���� ī�޶� Ÿ���� �ӽ� ������ �ֱ�
            t_dialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }

        // ������ �ӽ� ���� �����
        dialogueEvent.dialogues = t_dialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }

    public AppearType GetAppearType()
    {
        return dialogueEvent.appearType;
    }

    public GameObject[] GetAppearTargets()
    {
        return dialogueEvent.go_appearTargets;
    }

    public GameObject[] GetDisppearTargets()
    {
        return dialogueEvent.go_disappearTargets;
    }

    public GameObject GetNextEvent()
    {
        return dialogueEvent.go_nextEvent;
    }


    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theDM.SetNextEvent(GetNextEvent()); // ���� �̺�Ʈ ������ ����
    }

    private void Update()
    {
        // �ڵ� �̺�Ʈ�̰�, ������ �Ľ� �� ���̺� ��� ����Ǹ� (���� ����)
        if (isAutoEvent && DatabaseManager.isFinish)
        {
            DialogueManager.isWaiting = true;   // true �� �ڵ� �̺�Ʈ ���

            // ������Ʈ ����/����
            if (GetAppearType() == AppearType.Change)
            {
                theDM.SetAppearObjects(GetAppearTargets(), GetDisppearTargets());
            }

            theDM.ShowDialogue(GetDialogue());

            gameObject.SetActive(false);    // �ڵ� �̺�Ʈ�� �� ���� ���� ���� �ƿ� ��Ȱ��ȭ�Ѵ�.
        }
    }
}
