using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDirector_02 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //ȭ���� ������
        {
            SceneManager.LoadScene("MainScene_03"); //���� ������ �̵�
        }

    }
}
