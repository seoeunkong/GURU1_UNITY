using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneIndex : MonoBehaviour
{
    public static int Num;


    void Update()
    {
        Director();
    }

    public void Director()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4) //���� ���� �������� 1�̸�
        {         
            Num = 4;
            
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6) //���� ���� �������� 2�̸�
        {         
            Num = 6;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            Num = 9;
        }
       
    }
}
