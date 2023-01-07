using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  // 이동할 맵의 이름

    private PlayerMove thePlayer;   // 플레이어의 currentMapName 변수를 참조하기 위함

    void Start()
    {
        // FindObjectOfType<>: Hierachy에 있는 모든 객체의 <> 컴포넌트를 검색 후 리턴 (다수의 객체)
        // GetComponent<>: 해당 스크립트가 적용된 객체의 <> 컴포넌트를 검색 후 리턴 (단일 객체)
        // 검색 범위의 차이로 이해하면 O
        thePlayer = FindObjectOfType<PlayerMove>();
    }

    // box collider에 닿는 순간 실행되는 내장 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // box collider에 닿은 오브젝트의 이름 반환
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
