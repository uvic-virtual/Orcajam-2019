using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text menuHighScores;
    [SerializeField] private Camera playerCam;

    private GameObject player;
    private string[] highScores;

    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<CharacterController>().enabled = false;
        string path = Application.dataPath + "/Scripts/Player/HighScores.txt";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        menuHighScores.text = "-  High Scores  -\n" + content;

    }

    public void PlayButton()
    {
        IEnumerator temp = PlayButton(3f);
        player.GetComponent<CharacterController>().enabled = true;

        StartCoroutine(temp);
    }
    private IEnumerator PlayButton(float transSpeed)
    {
        for (int i = 0; i < 250 / transSpeed; i++)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * transSpeed, playerCam.transform);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("1.Game");
    }
}
