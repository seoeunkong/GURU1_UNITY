using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {

        Ready,
        Run,
        Pause,
        GameOver,
        StageClear
    }

    public GameState gState;
    AudioSource BG;
    public Text stateLabel;

    GameObject player;
    // PlayerMove playerM;
    public GameObject[] monster;
    int win = 0;
    public int index_num; //�� �ε���

    public static GameManager gm;
    public static int Num;
    public GameObject optionUI;

    public GameObject stage_clear;

    public GameObject howto;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        howto.SetActive(false);
        stage_clear.SetActive(false);
        gState = GameState.Ready;
        StartCoroutine(GameStart());
        player = GameObject.Find("Player");

        // playerM = player.GetComponent<PlayerMove>();
    }

    IEnumerator GameStart()
    {
        stateLabel.text = "Ready...";
        stateLabel.color = new Color32(233, 182, 12, 255);
        yield return new WaitForSeconds(2.0f);
        stateLabel.text = "Go";
        yield return new WaitForSeconds(0.5f);
        stateLabel.text = "";
        gState = GameState.Run;

    }

    // Update is called once per frame
    void Update()
    {
        Player player_stress = player.GetComponent<Player>();
        if (player_stress.Stress >= 100)
        {
            //stateLabel.text = "Game over....";
            // stateLabel.color = new Color32(255, 0, 0, 255);
            gState = GameState.GameOver;
            SceneManager.LoadScene("GAMEOVER");
        }


        for (int i = 0; i < monster.Length; i++)
        {

            if (monster[i] != null)
            {  //���� ���͵� ���� missing object�� �ƴ� ��� �����ϱ�
                return;
            }
            
            if (monster[i] == null || monster[i].activeSelf == false)
            {
                win++;
            }
            
        }
        Debug.Log(win);
        if (win >= monster.Length)
        {
            gState = GameState.StageClear;
            Debug.Log("gameclear");
            player.SetActive(false);
            StartCoroutine(WaitForIt());



        }
    }

    IEnumerator WaitForIt()
    {
        player.SetActive(false);
        stage_clear.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "GURU_STAGE1") //����  ���� ��������1�̶��
        {
            SceneManager.LoadScene("LOADING_STAGE02");
        }
        else if (SceneManager.GetActiveScene().name == "GURU_STAGE02")
        {
            SceneManager.LoadScene("LOADING_STAGE03");
        }
        else if (SceneManager.GetActiveScene().name == "GURU_STAGE03")
        {
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene("GURU_CINEMA");
        }
    }


    public void OpenOptionWindow()
    {
        stateLabel.text = "";
        gState = GameState.Pause;
        Time.timeScale = 0;
        BG=GameObject.Find("Main Camera").GetComponent<AudioSource>();
        BG.Pause();
        optionUI.SetActive(true);
    }

    public void CloseOptionWindow()
    {
        gState = GameState.Run;
        Time.timeScale = 1.0f;
        BG=GameObject.Find("Main Camera").GetComponent<AudioSource>();
        BG.Play();
        optionUI.SetActive(false);
        
    }

    public void GameRestart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        howto.SetActive(true);
      
    }


}
