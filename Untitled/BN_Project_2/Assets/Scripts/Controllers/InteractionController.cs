using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;    // �츮�� ���� �ִ� ī�޶�
    RaycastHit2D hit; // �������� ���� ���� ������Ʈ ������ ����

    [SerializeField] GameObject go_normalCursor;       // �Ϲ� Ŀ��
    [SerializeField] GameObject go_interactiveCursor;  // ��ȣ�ۿ� Ŀ��
    [SerializeField] GameObject go_movableCursor;      // �� �̵� Ŀ��

    // �� �� �ϳ��� ������ true
    bool isInteractive = false; // ��ȣ�ۿ� ���� ������Ʈ�� �����ϴ� ������ ������ true�� ����
    bool isMovable = false;     // �̵� ��ȣ�ۿ� ���� ������Ʈ�� �����ϴ� ������ ������ true�� ����

    public static bool clickedInteractive = false;    // ��ȣ�ۿ� ���� ������Ʈ�� Ŭ���ߴ���

    DialogueManager dm;

    [SerializeField] GameObject go_ui_cursor;  // UI_Cursor
    [SerializeField] GameObject go_ui_status;  // (�ӽ�) ����â


    // ���â�� ������, UI ����� (DialogueManager.cs���� ȣ��)
    // ��ȭ�� ������, UI ���̱�
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
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f); // ���콺 ��ġ���� �������� ���
        
        Debug.DrawRay(pos, transform.forward, Color.red, 0.3f);

        if (hit)
        {
            // Debug.Log(hit.transform.name);
            Contact();
        }

        else
        {
            NotContact();
        }
    }

    // �������� ������Ʈ�� �¾��� �� ����
    void Contact()
    {
        // ��ȣ�ۿ��� ������ ��ü�� Ŀ���� �̵����� �� ����
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

        // �̵� ��ȣ�ۿ��� ������ ��ü�� Ŀ���� �̵����� �� ����
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

    // �������� ������Ʈ�� ���� �ʾ��� �� ����
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

    // ��ȣ�ۿ� ���� ������Ʈ�� ��Ŭ������ ��, Interact �Լ� ȣ��
    void LeftClick()
    {
        // clickedInteractive ���� false�� ���� ������Ʈ�� Ŭ�� ����
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

    // ��ȣ�ۿ� ���� ������Ʈ�� ��Ŭ������ ��, ���â�� ������.
    // ���� ��Ŭ������ ���� ���� �̵���.
    void Interact()
    {
        clickedInteractive = true;

        InteractionEvent t_event = hit.transform.GetComponent<InteractionEvent>();

        // ������Ʈ Ŭ��
        if (hit.transform.GetComponent<InteractionType>().isObject)
        {
            CallDialogue(t_event);  // ��ȭ �̺�Ʈ ȣ��
        }
        
        // �� Ŭ��
        else
        {
            CallTransfer();
        }
    }

    // ��ȭ �̺�Ʈ ȣ��
    void CallDialogue(InteractionEvent p_event)
    {
        if (p_event.GetAppearType() == AppearType.Change)
        {
            dm.SetAppearObjects(p_event.GetAppearTargets(), p_event.GetDisppearTargets());
        }

        dm.ShowDialogue(p_event.GetDialogue()); // ��ȣ�ۿ��� ������Ʈ�� ��� �̺�Ʈ�� �����´�.
    }

    // ��(��) �̵� �Լ� ȣ��
    void CallTransfer()
    {
        string t_sceneName = hit.transform.GetComponent<InteractionDoor>().GetSceneName();
        string t_locationName = hit.transform.GetComponent<InteractionDoor>().GetLocationName();

        StartCoroutine(FindObjectOfType<TransferManager>().Transfer(t_sceneName, t_locationName));
    }
}
