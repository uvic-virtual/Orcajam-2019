using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForm : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] GameObject parent1;
    [SerializeField] GameObject parent2;
    [SerializeField] GameObject parent3;



    // Start is called before the first frame update
    void Start()
    {
        GameObject[] floorOneTiles = new GameObject[400];
        int writePos = 0;
        for(int x = -10; x <= 10; x++)
        {
            for(int z = -10; z <= 10; z++)
            {
                floorOneTiles[writePos] = Instantiate(tile, new Vector3(x, 0, z), Quaternion.identity,parent1.transform);
            }
        }

        GameObject[] floorTwoTiles = new GameObject[400];
        writePos = 0;
        for (int x = -10; x <= 10; x++)
        {
            for (int z = -10; z <= 10; z++)
            {
                floorTwoTiles[writePos] = Instantiate(tile, new Vector3(x, -5, z), Quaternion.identity, parent2.transform);
            }
        }

        GameObject[] floorThreeTiles = new GameObject[400];
        writePos = 0;
        for (int x = -10; x <= 10; x++)
        {
            for (int z = -10; z <= 10; z++)
            {
                floorThreeTiles[writePos] = Instantiate(tile, new Vector3(x, -10, z), Quaternion.identity, parent3.transform);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
