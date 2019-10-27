using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForm : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int size = 6;
    [SerializeField] private float dissapearTime = 1f;

    [SerializeField] private Transform parent1;
    [SerializeField] private Transform parent2;
    [SerializeField] private Transform parent3;

    private List<GameObject> floorOneTiles;
    private List<GameObject> floorTwoTiles;
    private List<GameObject> floorThreeTiles;

    private GameObject player;
    private float timer;

    // generate all 3 plateforms
    void Start()
    {
        player = GameObject.Find("Player");
        timer = 0f;

        floorOneTiles = new List<GameObject>();
        floorTwoTiles = new List<GameObject>();
        floorThreeTiles = new List<GameObject>();

        for(int x = -size; x <= size; x++)
        {
            for(int z = -size; z <= size; z++)
            {
                floorOneTiles.Add(Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity,parent1));
            }
        }

        for (int x = -size; x <= size; x++)
        {
            for (int z = -size; z <= size; z++)
            {
                floorTwoTiles.Add(Instantiate(tile, new Vector3(x, -5, z), Quaternion.identity, parent2));
            }
        }

        for (int x = -size; x <= size; x++)
        {
            for (int z = -size; z <= size; z++)
            {
                floorThreeTiles.Add(Instantiate(tile, new Vector3(x, -10, z), Quaternion.identity, parent3));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float playerPos = player.transform.position.y;
        int level = 0;
        if(playerPos > -1f && playerPos <= 5f)
        {
            level = 1;
        }
        if (playerPos > -6f && playerPos <= -2f)
        {
            level = 2;
        }
        if (playerPos > -11f && playerPos <= -7f)
        {
            level = 3;
        }
        TileDestroy(level);
    }

    private void TurnRed(GameObject target)
    {
        Renderer rend = target.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.red);
    }

    private void TileDestroy(int level)
    {
        timer += Time.deltaTime;
        if(timer < dissapearTime)
        {
            return;
        }

        int num = 0;


        switch (level)
        {
            case 1:
                num = RanNum(floorOneTiles.Count);
                Destroy(floorOneTiles[num],2);
                TurnRed(floorOneTiles[num]);
                floorOneTiles.RemoveAt(num);
                break;
            case 2:
                num = RanNum(floorTwoTiles.Count);
                Destroy(floorTwoTiles[num],2);
                TurnRed(floorTwoTiles[num]);
                floorTwoTiles.RemoveAt(num);
                break;

            //at this point I realized I should have all the floors in an array
            case 3:
                num = RanNum(floorThreeTiles.Count);
                Destroy(floorThreeTiles[num],2);
                TurnRed(floorThreeTiles[num]);
                floorThreeTiles.RemoveAt(num);
                break;
        }

        timer = 0f;
        return;
    }

    private int RanNum(int limit)
    {
        return Random.Range(0,limit);
    }
}
