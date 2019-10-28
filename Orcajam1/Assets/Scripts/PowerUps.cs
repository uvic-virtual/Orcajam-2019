using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private int platformSize = 8;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private GameObject[] items;

    private float timer;
    private GameObject player;
    void Start()
    {
        timer = 0;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTime)
        {
            timer = 0f;
            float height = player.transform.position.y;
            if (height > 5f)
                return;
            float itemY = 0.5f;
            if (height > -1f && height <= 5f)
            {
                itemY = 0.5f;
            }
            if (height > -6f && height <= -2f)
            {
                itemY = -4.5f;
            }
            if (height > -11f && height <= -7f)
            {
                itemY = -9.5f;
            }
            Spawn(new Vector3(RanNum(), itemY, RanNum()));
        }
    }

    private void Spawn(Vector3 pos)
    {
        Debug.Log(pos);
        int index = Random.Range(0, items.Length - 1);
        GameObject clone = Instantiate(items[index], pos,Quaternion.identity);
        Destroy(clone, 5f);
    }

    private int RanNum()
    {
        return Random.Range(-platformSize, platformSize);
    }


}
