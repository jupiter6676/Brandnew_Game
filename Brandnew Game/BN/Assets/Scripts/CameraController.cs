using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float speed;
    public float clampLeft;
    public float clampRight;
    public float clampUp;

    private float cameraX;


    static public CameraController instance;

    /* 카메라 범위 지정 */
    public BoxCollider2D bound; // 카메라 범위
    // 박스 콜라이더 영역의 최소, 최대 xyz값을 지님
    private Vector3 minBound;
    private Vector3 maxBound;
    // 카메라의 중심이 가운데에 있어, 영역 너비와 높이의 반을 빼준다.
    private float halfWidth;
    private float halfHeight;
    private Camera theCamera;   // 카메라의 반 높이 값을 구할 속성을 이용하기 위한 변수//

    private void Awake()
    {
        /* 카메라 범위 지정 */
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.fieldOfView;
        halfWidth = halfHeight * Screen.width / Screen.height;  // 공식
    }

    // Use this for initialization
    void Start () {
        cameraX = transform.position.x;

        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
	
	// Update is called once per frame
	void Update () {
        /* 카메라 가두기 */
        //float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        //float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        // this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);

        /*
        // if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < clampRight)
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < clampedX)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        // if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > clampLeft)
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x < clampedX)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        // if (Input.GetKey(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space) && transform.position.y < clampedY)
        {
            Debug.Log(cameraX);
        }
        */

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < clampRight)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > clampLeft)
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        // if (Input.GetKey(KeyCode.Space))
        if (Input.GetKey(KeyCode.Space) && transform.position.y < clampUp)
        {
            Debug.Log(cameraX);
        }
    }
}
