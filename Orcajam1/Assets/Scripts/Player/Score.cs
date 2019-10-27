﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text display;
    [SerializeField] private Text menuHighScores;

    private GameObject player;
    private CharacterController controller;
    private string[] highScores;
    private uint currScore;

    void Start()
    {
        player = GameObject.Find("Player");
        controller = player.GetComponent<CharacterController>();
        controller.enabled = false;

        display.text = "Score: 0";
        currScore = 0;
        //read from file
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        menuHighScores.text = "-  High Scores  -\n" + content;
        
        highScores = content.Split('\n');
        //print(highScores[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < 5f)
        {
            updateScore();
        }     
    }

    private void updateScore()
    {
        currScore ++;
        display.text = "Score: "+ currScore;
    }
}
