using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] bool isAutoEvent = false;  // true → 자동으로 실행되는 이벤트

    [SerializeField] DialogueEvent dialogueEvent;

    DialogueManager theDM;

    // 이 이벤트(캐릭터)를 등장시킬지 말지 결정하는 함수
    // 이벤트 종료 후 캐릭터를 퇴장시키는 코드는 Update 함수에 작성
    bool CheckEvent()
    {
        bool t_flag = true; // true면 등장, false면 등장 X

        for (int i = 0; i < dialogueEvent.eventTiming.eventConditions.Length; i++)
        {
            // 1. 등장 조건과 본 이벤트가 일치하지 않으면, 캐릭터 등장 X
            // 조건 이벤트가 실행되었는지의 여부 (true, false)
            // 해당 이벤트를 보아야 등장하는지의 여부 (true, false)
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventConditions[i]] != dialogueEvent.eventTiming.conditionFlag)
            {
                t_flag = false;
                break;
            }

            // 2. 캐릭터 퇴장 조건이 되는 이벤트를 보았다면, 캐릭터 등장 X
            if (DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventEndNum])
            {
                t_flag = false; // 캐릭터 등장 X
            }
        }

        return t_flag;
    }

    // DatabaseManager에 저장된 실제 대사 데이터를 꺼내온다.
    public Dialogue[] GetDialogue()
    {
        // 상호작용 전 대화
        if (!DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum])
        {
            DatabaseManager.instance.eventFlags[dialogueEvent.eventTiming.eventNum] = true; // 현재 이벤트의 실행 여부를 true로 설정
            dialogueEvent.dialogues = SetDialogue(dialogueEvent.dialogues, (int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

            return dialogueEvent.dialogues;
        }

        // 상호작용 후의 대화 (있을 경우)
        else if (dialogueEvent.dialoguesAfter.Length > 0)
        {
            dialogueEvent.dialoguesAfter = SetDialogue(dialogueEvent.dialoguesAfter, (int)dialogueEvent.lineAfter.x, (int)dialogueEvent.lineAfter.y);

            return dialogueEvent.dialoguesAfter;
        }

        // 상호작용 후의 대화가 없을 경우, 처음 대화를 계속 출력한다.
        // 근데 쓸 일은 없을 것 같긴 하다.
        return dialogueEvent.dialogues;
    }

    // 상호작용 전후 대사를 세팅 (p_dialogue: 상호작용 전 또는 후 대사)
    Dialogue[] SetDialogue(Dialogue[] p_dialogue, int p_lineX, int p_lineY)
    {
        Dialogue[] t_dialogues = DatabaseManager.instance.GetDialogue(p_lineX, p_lineY);

        for (int i = 0; i < t_dialogues.Length; i++)
        {
            // dialogueEvent에 넣은 Standing Image 오브젝트를 임시 변수에 넣기
            // t_dialogues[i].tf_standing = p_dialogue[i].tf_standing;

            // dialogueEvent에 넣은 카메라 타입을 임시 변수에 넣기
            t_dialogues[i].cameraType = p_dialogue[i].cameraType;
        }

        return t_dialogues;
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

    // 해당 이벤트의 번호를 가져오는 함수
    public int GetEventNum()
    {
        return dialogueEvent.eventTiming.eventNum;
    }

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theDM.SetNextEvent(GetNextEvent()); // 다음 이벤트 정보를 저장

        bool t_flag = CheckEvent(); // 이 이벤트(캐릭터)를 등장시킬지
        gameObject.SetActive(t_flag);
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
