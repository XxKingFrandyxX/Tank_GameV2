using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text m_timeTxt;
    public Text m_enemyTanksTxt;
    public Text m_bestTimeTxt;
    public Text m_messageTxt;
    public GameObject[] m_Tanks;
    private float m_gameTime = 0;
    public int[] bestTimes = new int[10];
    public HighScores m_HighScores;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;

    private void Awake()
    {
        m_GameState = GameState.Start;
        m_messageTxt.text = "Press Enter to Start";
    }

    void SetTanksEnable(bool enabled)
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(enabled);
        }
    }

    void Start()
    {
        SetTanksEnable(false);
        bestTimes = m_HighScores.GetScores();
        m_bestTimeTxt.text = bestTimes[0].ToString();
        int minutes = Mathf.FloorToInt(bestTimes[0] / 60f);
        int seconds = Mathf.FloorToInt(bestTimes[0] % 60);
        m_bestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void Update()
    {

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch(m_GameState)
        {
            case GameState.Start:
                GS_Start();
                break;
            case GameState.Playing:
                GS_Playing();
                break;
            case GameState.GameOver:
                GS_GameOver();
                break;
        }
    }

    void GS_Start()
    {
        Debug.Log("In Start Game State");
        if(Input.GetKeyUp(KeyCode.Return) == true)
        {
            m_GameState = GameState.Playing;
            m_enemyTanksTxt.text = (m_Tanks.Length - 1).ToString();
            SetTanksEnable(true);
            m_messageTxt.text = "";
        }
    }

    void GS_Playing()
    {
        Debug.Log("In Playing State");
        bool isGameOver = false;
        m_gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(m_gameTime / 60f);
        int seconds = Mathf.FloorToInt(m_gameTime % 60);
        m_timeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (isPlayerDead() == true)
        {
            isGameOver = true;
            m_messageTxt.text = "You Lose";
        }
        else if (OneTankLeft() == true)
        {
            isGameOver = true;
            Debug.Log("You Win!");
            SetTimes(Mathf.FloorToInt(m_gameTime));
        }

        if (isGameOver == true)
        {
            m_GameState = GameState.GameOver;
        }

        if (OneTankLeft() == true)
        {
            isGameOver = true;
            m_messageTxt.text = "You Win!";
            SetTimes(Mathf.FloorToInt(m_gameTime));
            m_HighScores.SetScores(bestTimes);
            minutes = Mathf.FloorToInt(bestTimes[0] / 60f);
            seconds = Mathf.FloorToInt(bestTimes[0] % 60);
            m_bestTimeTxt.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }

    }

    void GS_GameOver()
    {
        Debug.Log("In Game Over State");
        if(Input.GetKeyUp(KeyCode.Return) == true)
        {
            m_gameTime = 0;
            m_GameState = GameState.Playing;
            SetTanksEnable(false);
            SetTanksEnable(true);
        }
    }

    bool OneTankLeft()
    {
        int numTanksLeft = 1;
        for (int i = 1; i < m_Tanks.Length; i++)
        {
            if(m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        m_enemyTanksTxt.text = (numTanksLeft - 1).ToString();
        return numTanksLeft <= 1;
    }

    bool isPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if(m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                    return true;
            }
        }
        return false;
    }

    void SetTimes(int newTime)
    {
        if (newTime <= 0)
            return;
        int tempTime;
        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (bestTimes[i] > newTime || bestTimes[i] == 0)
            {
                tempTime = bestTimes[i];
                bestTimes[i] = newTime;
                newTime = tempTime;
            }       
        }
        Debug.Log("Time to beat = " + bestTimes[0]);
    }
}

    

