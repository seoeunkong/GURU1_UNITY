using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ClearDirector_04 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //ȭ���� ������
        {
            gameObject.SetActive(false);
        }

    }
}
