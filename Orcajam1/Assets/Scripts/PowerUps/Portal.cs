using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
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
        other.gameObject.transform.position = new Vector3(0, 15, 0);
    }
}
