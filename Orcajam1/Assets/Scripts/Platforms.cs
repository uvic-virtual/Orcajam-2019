using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private int Size = 6;
    [SerializeField] private int DistanceBetweenPlatforms = 5;

    private Transform Player;

    private List<List<GameObject>> Layers;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Layers = new List<List<GameObject>>
        {
            MakeLevel(0)
        };
    }

    private void Update()
    {
        int nextPlatform = Mathf.RoundToInt(Player.transform.position.y / DistanceBetweenPlatforms) * DistanceBetweenPlatforms;
        //Layers.Add(MakeLevel(nextPlatform));
    }

    private List<GameObject> MakeLevel(int y)
    {
        var level = new List<GameObject>();

        for (int x = -Size; x <= Size; x++)
        {
            for (int z = -Size; z <= Size; z++)
            {
                level.Add(Instantiate(TilePrefab, new Vector3(x, y, z), Quaternion.identity));
            }
        }
        return level;
    }
}


