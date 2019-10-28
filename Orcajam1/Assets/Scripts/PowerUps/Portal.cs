using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = new Vector3(0, 15, 0);
    }
}
