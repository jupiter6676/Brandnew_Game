using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
    // Ÿ���� ����� ������ �ٲ��, �������� �ڷ�ƾ���� ���� �� �ִ�.
    // �̹� �������� �ڷ�ƾ�� �ߴ��ؾ� �Ѵ�.
    public void CameraTragetting(Transform p_target, float p_camSpeed = 0.05f)
    {
        if (p_target != null)
        {
            StopAllCoroutines();
            CameraTargettingCoroutine(p_target, p_camSpeed);
        }
    }

    // p_target: ī�޶� Ÿ������ ���, p_camSpeed: ī�޶� �̵� �ӵ�
    IEnumerator CameraTargettingCoroutine(Transform p_target, float p_camSpeed = 0.05f)
    {
        Vector3 t_targetPos = p_target.position;    // ����� ��ġ

        // ����� ������ ��ġ �� ī�޶� ���� ��ġ�� �ʰ� ������ ����� ���̵���
        Vector3 t_targetFrontPos = t_targetPos + p_target.forward;    // ����� ��ġ + �Ķ��� �� ����
        Vector3 t_direction = (t_targetPos - t_targetFrontPos).normalized;  // ī�޶� �ٶ󺸴� ����

        // ī�޶��� ��ġ�� ������(����� ����)�� �������� �ʾ����� �� ī�޶� �̵�
        // ī�޶� ������ ����� �ٶ󺸴� ������ ���� 0.5f �̻��̸� �� ī�޶� ȸ��
        while (transform.position != t_targetFrontPos || Quaternion.Angle(transform.rotation, Quaternion.LookRotation(t_direction)) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t_targetFrontPos, p_camSpeed); // ī�޶� �̵�
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t_direction), p_camSpeed); // ī�޶� ȸ��

            yield return null;
        }
    }
    */
}
