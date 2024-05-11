using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class HighScores : MonoBehaviour
{
    public string scoreFileName = "highscore.txt";
    string currentDirectory;
    int[] scores = new int[10];

    public void LoadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if (fileExists == true)
        {
            Debug.Log("Found highscore file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName + " Does not exist, no scores loaded");
            return;
        }
        scores = new int[scores.Length];

        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFileName);

        int scoreCount = 0;

        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileline = fileReader.ReadLine();

            int readScore = -1;
            bool didparse = int.TryParse(fileline, out readScore);

            if (didparse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                Debug.Log("Invaild line in scores file at " + scoreCount + ", using default value.");
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        fileReader.Close();
    }

    public void SaveScoresToFile()
    {
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }
        fileWriter.Close();
        Debug.Log("Sucessfully written to file");
    }

    void Awake()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Our current Directory is: " + currentDirectory);
    }

    public void SetScores(int[] newScores)
    {
        scores = newScores;
        SaveScoresToFile();
    }

    public int[] GetScores()
    {
        LoadScoresFromFile();
        return scores;
    }
}
