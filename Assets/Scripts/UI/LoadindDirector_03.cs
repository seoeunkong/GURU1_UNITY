using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadindDirector_03 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //��ư�� ������
        {
            SceneManager.LoadScene("GURU_STAGE03"); //���� ������ �̵�
        }

    }
}
