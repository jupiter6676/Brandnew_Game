using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;    // �츮�� ���� �ִ� ī�޶�
    RaycastHit2D hit; // �������� ���� ���� ������Ʈ ������ ����

    [SerializeField] GameObject go_normalCursor;   // �Ϲ� Ŀ��
    [SerializeField] GameObject go_interactiveCursor;  // ��ȣ�ۿ� Ŀ��
    [SerializeField] GameObject go_movableCursor;   // �� �̵� Ŀ��

    bool isInteractive = false; // ��ȣ�ۿ� ���� ������Ʈ�� �����ϴ� ������ ������ true�� ����
    bool isMovable = false; // �̵� ��ȣ�ۿ� ���� ������Ʈ�� �����ϴ� ������ ������ true�� ����

    public static bool clickedInteractive = false;    // ��ȣ�ۿ� ���� ������Ʈ�� Ŭ���ߴ���

    DialogueManager dm;

    [SerializeField] GameObject go_ui_cursor;  // UI_Cursor
    [SerializeField] GameObject go_ui_status;  // (�ӽ�) ����â


    // ���â�� ������, UI ����� (DialogueManager.cs���� ȣ��)
    public void HideUI()
    {
        go_ui_cursor.SetActive(false);
        go_ui_status.SetActive(false);
    }


    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        CheckObject();
        LeftClick();
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
            // isContact == false�� ���� ����
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
            // isContact == false�� ���� ����
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
                if (isInteractive)
                {
                    Interact();
                }
            }
        }
    }

    // ��ȣ�ۿ� ���� ������Ʈ�� ��Ŭ������ ��, ���â�� ������.
    void Interact()
    {
        clickedInteractive = true;

        dm.ShowDialogue();
    }
}
