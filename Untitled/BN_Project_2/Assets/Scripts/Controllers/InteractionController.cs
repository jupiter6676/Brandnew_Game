using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // [SerializeField] Camera cam;    // 우리가 보고 있는 카메라
    RaycastHit2D hit; // 레이저를 쏴서 맞춘 오브젝트 정보를 저장

    [SerializeField] GameObject go_normalCursor;       // 일반 커서
    [SerializeField] GameObject go_interactiveCursor;  // 상호작용 커서
    [SerializeField] GameObject go_movableCursor;      // 맵 이동 커서

    // 둘 중 하나는 무조건 true
    bool isInteractive = false; // 상호작용 가능 오브젝트에 접촉하는 최초의 순간에 true로 변경
    bool isMovable = false;     // 이동 상호작용 가능 오브젝트에 접촉하는 최초의 순간에 true로 변경

    public static bool clickedInteractive = false;    // 상호작용 가능 오브젝트를 클릭했는지

    DialogueManager dm;

    [SerializeField] GameObject go_ui_cursor;  // UI_Cursor
    [SerializeField] GameObject go_ui_status;  // (임시) 상태창


    public static InteractionController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    // 대사창이 나오면, UI 숨기기 (DialogueManager.cs에서 호출)
    // 대화가 끝나면, UI 보이기
    public void SettingUI(bool p_flag)
    {
        go_ui_cursor.SetActive(p_flag);
        go_ui_status.SetActive(p_flag);

        clickedInteractive = !p_flag;
    }

    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (!clickedInteractive)
        {
            CheckObject();
            LeftClick();
        }
    }

    void CheckObject()
    {
        // Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 신 이동 시 메인 카메라를 자동으로 잡아줌.
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f); // 마우스 위치에서 레이저를 쏘기
        
        Debug.DrawRay(pos, transform.forward, Color.red, 0.3f);

        if (hit)
        {
            Contact();
        }

        else
        {
            NotContact();
        }
    }

    // 레이저에 오브젝트가 맞았을 때 실행
    void Contact()
    {
        // 상호작용이 가능한 물체에 커서를 이동했을 때 실행
        if (hit.transform.CompareTag("Interaction"))
        {
            if (!isInteractive)
            {
                isInteractive = true;
                isMovable = false;

                go_interactiveCursor.SetActive(true);
                go_normalCursor.SetActive(false);
                go_movableCursor.SetActive(false);
            }
        }

        // 이동 상호작용이 가능한 물체에 커서를 이동했을 때 실행
        else if (hit.transform.CompareTag("Move_Interaction"))
        {
            if (!isMovable)
            {
                isMovable = true;
                isInteractive = false;

                go_interactiveCursor.SetActive(false);
                go_normalCursor.SetActive(false);
                go_movableCursor.SetActive(true);
            }
        }

        else
        {
            NotContact();
        }
    }

    // 레이저에 오브젝트가 맞지 않았을 때 실행
    void NotContact()
    {
        if (isMovable || isInteractive)
        {
            isMovable = false;
            isInteractive = false;
            
            go_interactiveCursor.SetActive(false);
            go_normalCursor.SetActive(true);
            go_movableCursor.SetActive(false);
        }
    }

    // 상호작용 가능 오브젝트를 좌클릭했을 때, Interact 함수 호출
    void LeftClick()
    {
        // clickedInteractive 값이 false일 때만 오브젝트를 클릭 가능
        if (!clickedInteractive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isInteractive || isMovable)
                {
                    Interact();
                }
            }
        }
    }

    // 상호작용 가능 오브젝트를 좌클릭했을 때, 대사창을 보여줌.
    // 문을 좌클릭했을 때는 맵을 이동함.
    void Interact()
    {
        clickedInteractive = true;

        InteractionEvent t_event = hit.transform.GetComponent<InteractionEvent>();

        // 오브젝트 클릭
        if (hit.transform.GetComponent<InteractionType>().isObject)
        {
            CallDialogue(t_event);  // 대화 이벤트 호출
        }
        
        // 문 클릭
        else
        {
            CallTransfer();
        }
    }

    // 대화 이벤트 호출
    void CallDialogue(InteractionEvent p_event)
    {
        if (p_event.GetAppearType() == AppearType.Change)
        {
            dm.SetAppearObjects(p_event.GetAppearTargets(), p_event.GetDisppearTargets());
        }

        dm.ShowDialogue(p_event.GetDialogue()); // 상호작용한 오브젝트의 대사 이벤트를 꺼내온다.
    }

    // 맵(신) 이동 함수 호출
    void CallTransfer()
    {
        string t_sceneName = hit.transform.GetComponent<InteractionDoor>().GetSceneName();
        string t_locationName = hit.transform.GetComponent<InteractionDoor>().GetLocationName();

        StartCoroutine(FindObjectOfType<TransferManager>().Transfer(t_sceneName, t_locationName));
    }
}
