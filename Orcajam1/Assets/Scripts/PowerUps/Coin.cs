using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            score.AddScore(100);
            Destroy(gameObject);
        }
    }
}
