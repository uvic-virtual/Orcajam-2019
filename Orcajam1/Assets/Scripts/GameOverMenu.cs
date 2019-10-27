using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text menuHighScores;
    [SerializeField] private Text currentScore;
    [SerializeField] private InputField inputField;


    private string[] highScores;
    private string playerName;
    private bool highEnough;
    private int currScore;

    void Start()
    {
        highEnough = false;

        string path = Application.dataPath + "/Scripts/Player/PlayerScore.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        currScore = Int32.Parse(content);
        reader.Close();

        currentScore.text = "Your Score is: " + content;
        path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        reader = new StreamReader(path);
        content = reader.ReadToEnd();
        reader.Close();
        menuHighScores.text = "-  High Scores  -\n" + content;
        highScores = content.Split('\n');

        string temp = highScores[highScores.Length - 1];
        string[] temp2 = temp.Split(' ');
        int oldLowest = Int32.Parse(temp2[1]);
        if (currScore > oldLowest)
            highEnough = true;
        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnterButton()
    {
        playerName = inputField.text;
        if (highEnough == false)
            return;
        UpdateHighScores();

        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamWriter writer = new StreamWriter(path);
        writer.Write("");
        writer.Close();
        writer = new StreamWriter(path,true);

        for (int i = 0;i < highScores.Length; i++)
        {
            writer.Write(highScores[i]);
            if(i != highScores.Length - 1)
                writer.Write('\n');
        }     
        writer.Close();

        path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        menuHighScores.text = "-  High Scores  -\n" + content;
    }
    private void Swap(int a,int b)
    {
        string temp = highScores[a];
        highScores[a] = highScores[b];
        highScores[b] = temp;
    }
    private void UpdateHighScores()
    {
        highScores[highScores.Length - 1] = playerName + ' ' + currScore.ToString();
        string[] temp;
        int tempScore;
        for(int i = highScores.Length - 1; i >= 0; i--)
        {
            temp = highScores[i-1].Split(' ');
            tempScore = Int32.Parse(temp[1]);
            if(currScore >= tempScore)
            {
                Swap(i, i - 1);
            }
            else
            {
                return;
            }
        }
    }




    public void RestartButton()
    {
        SceneManager.LoadScene("0.Menu");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
