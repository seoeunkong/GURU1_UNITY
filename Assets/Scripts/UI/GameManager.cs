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

    public static GameManager gm;
    public GameObject optionUI;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
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
            Debug.Log("gameOver");
        }


        for (int i = 0; i < monster.Length; i++)
        {

            if (monster[i] != null)
            {  //만약 몬스터들 값이 missing object가 아닌 경우 제외하기
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

        }
    }

    public void OpenOptionWindow()
    {
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


}
