using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_restart : MonoBehaviour
{
    int sceneIndex;
    public GameObject num;

   
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) //��ư�� ������
        {
            Time.timeScale = 1.0f;

            if ((SceneIndex.Num) == 4)
            {
                SceneManager.LoadScene("GURU_STAGE1");
            }
            else if ((SceneIndex.Num) == 6)
            {
                SceneManager.LoadScene("GURU_STAGE02");
            }
            else if ((SceneIndex.Num) == 6)
            {
                SceneManager.LoadScene("GURU_STAGE03");
            }


        }

    }
}
