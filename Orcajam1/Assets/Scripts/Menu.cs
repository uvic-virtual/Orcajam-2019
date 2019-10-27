using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text menuHighScores;

    private string[] highScores;

    void Start()
    {
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        menuHighScores.text = "-  High Scores  -\n" + content;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        StartCoroutine(PlayButton_wait);
    }
    private IEnumerable PlayButton_wait()
    {
        gameObject.transform.parent = null;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("1.Game");
    }
}
