using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text display;

    private string[] highScores;
    private uint currScore;

    void Start()
    {
        display.text = "Score: 0";
        currScore = 0;
        //read from file
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        highScores = content.Split('\n');
        //print(highScores[0]);
    }

    // Update is called once per frame
    void Update()
    {
        updateScore();
    }

    private void updateScore()
    {
        currScore ++;
        display.text = "Score: "+ currScore;
    }
}
