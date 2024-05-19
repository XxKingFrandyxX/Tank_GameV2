using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text m_timeTxt;
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
            SetTanksEnable(true);
        }
    }

    void GS_Playing()
    {
        Debug.Log("In Playing State");
        bool isGameOver = false;
        m_gameTime += Time.deltaTime;

        if (isPlayerDead() == true)
        {
            isGameOver = true;
            Debug.Log("You Lose");
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
            Debug.Log("You Win!");
            SetTimes(Mathf.FloorToInt(m_gameTime));
            m_HighScores.SetScores(bestTimes);
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
        int numTanksLeft = 0;
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if(m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
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

    

