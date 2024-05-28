using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menuUI : MonoBehaviour
{
    public GameObject exitMenu;
    public TMP_Text m_HighScoresMsg;
    public HighScores m_highScores;
    int[]  m_Scores;
    public void PlayGame(string sceneName)
    {
        Debug.Log("Play Game");

        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
  
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ExitMenu(bool exiting)
    {
        exitMenu.SetActive(exiting);
    }

    void DisplayHighScores()
    {
        m_Scores = m_highScores.GetScores();

        string text = "";
        for (int i = 0; i < m_Scores.Length; i++)
        {
            int seconds = m_Scores[i];
            text += string.Format("{0:D2}:{1:D2}\n",
                          (seconds / 60), (seconds % 60));
        }
        m_HighScoresMsg.text = text;
    }

    private void Start()
    {
        DisplayHighScores();
    }
}