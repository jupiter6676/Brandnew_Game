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


    void Update()
    {
        CheckObject();
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
}
