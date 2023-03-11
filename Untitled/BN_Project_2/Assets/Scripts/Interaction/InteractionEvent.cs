using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;  // true → 자동으로 실행되는 이벤트

    [SerializeField] DialogueEvent dialogueEvent;

    DialogueManager theDM;

    // DatabaseManager에 저장된 실제 대사 데이터를 꺼내온다.
    public Dialogue[] GetDialogue()
    {
        DialogueEvent t_dialogueEvent = new DialogueEvent();    // 임시 변수
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            // dialogueEvent에 넣은 Standing Image 오브젝트를 임시 변수에 넣기
            // t_dialogueEvent.dialogues[i].tf_standing = dialogueEvent.dialogues[i].tf_standing;

            // dialogueEvent에 넣은 카메라 타입을 임시 변수에 넣기
            t_dialogueEvent.dialogues[i].cameraType = dialogueEvent.dialogues[i].cameraType;
        }

        // 원본에 임시 변수 덮어쓰기
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
        theDM.SetNextEvent(GetNextEvent()); // 다음 이벤트 정보를 저장
    }

    private void Update()
    {
        // 자동 이벤트이고, 데이터 파싱 후 테이블에 모두 저장되면 (오류 방지)
        // + 이동이 모두 끝났다면 (이동을 안하는 경우도 기본값이 true이므로 자동 이벤트가 있으면 내부가 실행됨)
        if (isAutoEvent && DatabaseManager.isFinish && TransferManager.isFinished)
        {
            DialogueManager.isWaiting = true;   // true → 자동 이벤트 대기

            // 오브젝트 등장/퇴장
            if (GetAppearType() == AppearType.Change)
            {
                theDM.SetAppearObjects(GetAppearTargets(), GetDisppearTargets());
            }

            theDM.ShowDialogue(GetDialogue());

            gameObject.SetActive(false);    // 자동 이벤트를 한 번만 보기 위해 아예 비활성화한다.
        }
    }
}
