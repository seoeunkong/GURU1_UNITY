using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingDirector_02 : MonoBehaviour
{
    private AudioSource buttonClick;

    void Start()
    {
        buttonClick = GetComponent<AudioSource>();
        buttonClick.Stop();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //버튼을 누르면
        {
            buttonClick.Play();
            SceneManager.LoadScene("GURU_STAGE02"); //다음 씬으로 이동
        }

    }
}
