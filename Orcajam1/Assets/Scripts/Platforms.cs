using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    [SerializeField] private int Size = 6;
    [SerializeField] private int MaxLevels = 10;
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
        //get height of parent block in current platform.
        float currentLevelHeight = Layers[Layers.Count -1][0].transform.position.y;

        //If player goes below block (and we have extra platforms), make another platform below it.
        if (Player.transform.position.y < currentLevelHeight && Layers.Count < MaxLevels)
        {
            Layers.Add(MakeLevel((int)currentLevelHeight - DistanceBetweenPlatforms));
        }
    }

    /// <summary>
    /// Creates a new layer of platforms to walk around on.
    /// </summary>
    /// <param name="y"> The y position of the platform.</param>
    /// <returns>A list of the gameobjects that make the platform.</returns>
    private List<GameObject> MakeLevel(int y)
    {
        //Create empty parent gameobject below this gameobject.
        var parent = new GameObject(("Level Parent " + y));
        parent.transform.position = new Vector3(transform.position.x, y, transform.position.z);

        //Create list of gameobjects and add parent to list.
        var level = new List<GameObject>
        {
            parent
        };

        //Create a square of tiles.
        for (int x = -Size; x <= Size; x++)
        {
            for (int z = -Size; z <= Size; z++)
            {
                level.Add(Instantiate(TilePrefab, new Vector3(x, y, z), Quaternion.identity, parent.transform));
            }
        }
        return level;
    }
}


