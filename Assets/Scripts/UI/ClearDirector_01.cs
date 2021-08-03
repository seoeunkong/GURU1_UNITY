using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDirector_01 : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //화면을 누르면
        {
            SceneManager.LoadScene("MainScene_02"); //다음 씬으로 이동
        }
        
    }
}
