using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadindDirector_03 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //버튼을 누르면
        {
            SceneManager.LoadScene("GURU_STAGE03"); //다음 씬으로 이동
        }

    }
}
