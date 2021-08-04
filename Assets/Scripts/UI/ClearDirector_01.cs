using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDirector_01 : MonoBehaviour
{
    private AudioSource buttonClick;

    void Start()
    {
        buttonClick = GetComponent<AudioSource>();
        buttonClick.Stop();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //ȭ���� ������
        {
            buttonClick.Play();
            SceneManager.LoadScene("MainScene_02"); //���� ������ �̵�
        }
        
    }
}
