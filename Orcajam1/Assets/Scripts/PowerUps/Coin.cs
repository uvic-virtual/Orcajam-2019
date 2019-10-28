using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject player;
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            score.AddScore(100);
            Destroy(gameObject);
        }
    }
}
