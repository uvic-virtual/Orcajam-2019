using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text display;

    private GameObject player;
    private uint currScore;

    void Start()
    {
        player = GameObject.Find("Player");
        display.text = "Score: 0";
        currScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 5f && player.transform.position.y > -17)
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        currScore++;
        display.text = "Score: " + currScore;
    }

    public uint GetScore()
    {
        return currScore;
    }
}
