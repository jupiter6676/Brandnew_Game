using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
    // 타겟팅 대상이 빠르게 바뀌면, 실행중인 코루틴끼리 꼬일 수 있다.
    // 이미 실행중인 코루틴을 중단해야 한다.
    public void CameraTragetting(Transform p_target, float p_camSpeed = 0.05f)
    {
        if (p_target != null)
        {
            StopAllCoroutines();
            CameraTargettingCoroutine(p_target, p_camSpeed);
        }
    }

    // p_target: 카메라가 타겟팅할 대상, p_camSpeed: 카메라 이동 속도
    IEnumerator CameraTargettingCoroutine(Transform p_target, float p_camSpeed = 0.05f)
    {
        Vector3 t_targetPos = p_target.position;    // 대상의 위치

        // 대상의 정면의 위치 → 카메라가 대상과 겹치지 않고 정면의 모습이 보이도록
        Vector3 t_targetFrontPos = t_targetPos + p_target.forward;    // 대상의 위치 + 파란색 축 길이
        Vector3 t_direction = (t_targetPos - t_targetFrontPos).normalized;  // 카메라가 바라보는 방향

        // 카메라의 위치가 목적지(대상의 정면)에 도달하지 않았으면 → 카메라 이동
        // 카메라 각도와 대상을 바라보는 각도의 차가 0.5f 이상이면 → 카메라 회전
        while (transform.position != t_targetFrontPos || Quaternion.Angle(transform.rotation, Quaternion.LookRotation(t_direction)) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, t_targetFrontPos, p_camSpeed); // 카메라 이동
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(t_direction), p_camSpeed); // 카메라 회전

            yield return null;
        }
    }
    */
}
