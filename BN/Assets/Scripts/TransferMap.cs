using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;  // 이동할 맵의 이름

    void Start()
    {
        
    }

    // box collider에 닿는 순간 실행되는 내장 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // box collider에 닿은 오브젝트의 이름 반환
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(transferMapName);
        }
    }
}
