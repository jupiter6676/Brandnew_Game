using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        Debug.Log("�� ����");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // GameScene���� ��ȯ
    }

    public void OnClickLoad()
    {
        Debug.Log("�ҷ�����");
    }

    public void OnClickOption()
    {
        Debug.Log("�ɼ�");
    }

    public void OnClickQuit()
    {
        Debug.Log("����");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ����� ������ ���α׷� ����
#endif
    }
}
