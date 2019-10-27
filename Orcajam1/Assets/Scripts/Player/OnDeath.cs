using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private Animator anime;
    [SerializeField] private Image health;

    private Score score;
    private GameObject player;
    private bool showEffects;
    void Start()
    {
        showEffects = false;
        player = GameObject.Find("Player");
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.y < -20 || health.fillAmount <= 0) && showEffects == false)
        {
            GameOver();
            showEffects = true;
        }
    }

    private void GameOver()
    {
        anime.SetTrigger("FadeIn");
        LogHighScores();
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void ChangeToGameOverScene()
    {
        SceneManager.LoadScene("2.GameOver");
    }

    private void LogHighScores()
    {
        uint currScore = score.GetScore();
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamWriter writer = new StreamWriter(path,true);
        writer.WriteLine();
        writer.WriteLine(currScore.ToString());
        writer.Close();
    }
}
