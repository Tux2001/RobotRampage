using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject gameOverPanel;

    public GameUI gameUI;
    public GameObject player;
    public int score;
    public int waveCountdown;
    public bool isGameOver;

    private static Game singleton;

    [SerializeField] RobotSpawn[] spawns;
    public int enemiesLeft;

    void Start()
    {
        singleton = this;
        StartCoroutine("increaseScoreEachSecond");
        isGameOver = false;
        Time.timeScale = 1;
        waveCountdown = 30;
        enemiesLeft = 0;
        StartCoroutine("updateWaveTimer");
        SpawnRobots();
    }

    //Goes through array and calls SpawnRobot()
    private void SpawnRobots()
    {
        foreach (RobotSpawn spawn in spawns)
        {
            spawn.SpawnRobot();
            enemiesLeft++;
        }
        gameUI.SetEnemyText(enemiesLeft);
    }

    private IEnumerator updateWaveTimer()
    {
        while(!isGameOver) 
        {
            yield return new WaitForSeconds(1f);
            waveCountdown--;
            gameUI.SetWaveText(waveCountdown);

            //Spawns the next wave and restarts countdown
            if(waveCountdown == 0)
            {
                SpawnRobots();
                waveCountdown = 30;
                gameUI.ShowNewWaveText();
            }
        }
    }

    public static void RemoveEnemy()
    {
        singleton.enemiesLeft--;
        singleton.gameUI.SetEnemyText(singleton.enemiesLeft);

        //Gives a bonus to player if wave is cleared early
        if (singleton.enemiesLeft == 0)
        {
            singleton.score += 50;
            singleton.gameUI.ShowWaveClearBonus();
        }
    }

    //Adds kills to scoreUI
    public void AddRobotKillToScore()
    {
        score += 10;
        gameUI.SetScoreText(score);
    }

    //Updates score every second
    IEnumerator increaseScoreEachSecond()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(1);
            score += 1;
            gameUI.SetScoreText(score);
        }
    }

    //Allows cursor on sceen to see options
    private void OnGUI()
    {
        if(isGameOver && Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }    
    }

    //sets timeScale to 0 so robots stop moving & Disables Controls
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        gameOverPanel.SetActive(true);
    }

    //3
    public void RestartGame()
    {
        SceneManager.LoadScene(Constants.SceneBattle);
        gameOverPanel.SetActive(true);
    }

    //4
    public void Exit()
    {
        Application.Quit();
    }

    //5
    public void MainMenu()
    {
        SceneManager.LoadScene(Constants.SceneMenu);
    }

}
