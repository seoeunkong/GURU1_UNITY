using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_restart : MonoBehaviour
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
            SceneManager.LoadScene(0);
            

        }

    }
}
