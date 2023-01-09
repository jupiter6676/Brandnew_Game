using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    /** SerializeField
     * private으로 되어있을 경우, inspector 창에서 tf_cursor를 조작할 수 X
     * → SerializeField를 쓰면, tf_cursor를 inspector 창에 띄워준다.
     * → 보호 수준은 유지되면서, public과 동일한 효과를 준다.
     */

    [SerializeField] Transform tf_cursor;   // 커서의 현재 위치
    
    /* 드래그 시 카메라 이동 */
    [SerializeField] float dragSpeed = 10.0f;   // 화면 움직임 속도
    private float firstClickPointX;

    // 변수명 정리가 필요할 듯..
    private float camWidth, camHeight;
    [SerializeField] Vector2 center;    // 0, 0
    [SerializeField] Vector2 mapSize;   // 배경 너비, 높이
    private RectTransform tf_background;    // 배경 너비, 높이를 가져오기 위한 변수


    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        tf_background = GameObject.Find("Background").GetComponent<RectTransform>();
        
        mapSize.x = tf_background.rect.width;
        mapSize.y = tf_background.rect.height;
    }

    void Update()
    {
        CursorMoving();
        // ViewMoving();
    }

    void CursorMoving()
    {
        // UI_Cursor 객체를 움직이려 하는데, 이 객체는 Game Canvas의 자식
        // position: 절대좌표, localPosition: 상대좌표 (부모 객체 기준)
        // tf_cursor.localPosition = Input.mousePosition;  // 현재 마우스의 위치

        // 마우스 이동
        float x = Input.mousePosition.x - (Screen.width / 2);
        float y = Input.mousePosition.y - (Screen.height / 2);
        tf_cursor.localPosition = new Vector2(x, y);

        // 마우스 가두기 (범위 지정)
        float tmp_cursorPosX = tf_cursor.localPosition.x;
        float tmp_cursorPosY = tf_cursor.localPosition.y;

        float min_width = -Screen.width / 2;
        float max_width = Screen.width / 2;
        float min_height = -Screen.height / 2;
        float max_height = Screen.height / 2;
        int padding = 20;

        tmp_cursorPosX = Mathf.Clamp(tmp_cursorPosX, min_width + padding, max_width - padding);
        tmp_cursorPosY = Mathf.Clamp(tmp_cursorPosY, min_height + padding, max_height - padding);
        
        tf_cursor.localPosition = new Vector2(tmp_cursorPosX, tmp_cursorPosY);
    }

    void ViewMoving()
    {
        // 마우스 최초 클릭 시의 위치 기억
        if (Input.GetMouseButtonDown(0))
        {
            firstClickPointX = tf_cursor.localPosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            // 현재 마우스 위치 - 최초 위치 의 음의 방향으로 카메라 이동
            Vector2 position = Camera.main.ScreenToViewportPoint(- new Vector3(tf_cursor.localPosition.x - firstClickPointX, 0, 0));
            Vector2 move = position * (Time.deltaTime * dragSpeed);

            Camera.main.transform.Translate(move);

            // 카메라 범위 제한
            float dx = mapSize.x - camWidth;
            float clampX = Mathf.Clamp(Camera.main.transform.position.x, -dx + center.x, dx + center.x);

            float dy = mapSize.y - camHeight;
            float clampY = Mathf.Clamp(Camera.main.transform.position.y, -dy + center.y, dy + center.y);

            Camera.main.transform.position = new Vector3(clampX, clampY, Camera.main.transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
