using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBalls : MonoBehaviour
{
    [SerializeField] GameObject fireBalls;
    private GameObject player;
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {

        }
    }
}
