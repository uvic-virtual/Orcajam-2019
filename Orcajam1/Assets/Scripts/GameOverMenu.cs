using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text menuHighScores;

    private string[] highScores;


    void Start()
    {
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        menuHighScores.text = "-  High Scores  -\n" + content;


    }

    // Update is called once per frame
    void Update()
    {
        
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
