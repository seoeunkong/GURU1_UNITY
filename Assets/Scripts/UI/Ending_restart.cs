using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_restart : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //��ư�� ������
        {
            
                SceneManager.LoadScene(0);
            

        }

    }
}
