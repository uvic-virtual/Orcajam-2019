using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;

    [SerializeField] private GameObject ZombiePrefab;

    /// <summary>
    /// 1/2 * the # of tiles on a side.</summary>
    [SerializeField] private int Size = 8;

    [SerializeField] private int MaxZombies = 10;

    /// <summary>
    /// Number of layers at start.</summary>
    [SerializeField] private int StartLevels = 3;

    [SerializeField] private float DistanceBetweenPlatforms = 5;

    /// <summary>
    /// How long a tile stays red before being destroyed.</summary>
    [SerializeField] private float RedToDestroyTime = 3;

    /// <summary>
    /// Delay before choosing a new tile to destroy.</summary>
    [SerializeField] private float TileDestroyDelay = 0.5f;

    private Transform Player;

    private List<List<GameObject>> Layers;

    private List<GameObject> Zombies;

    private float Timer;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Layers = new List<List<GameObject>>();
        for (int i=0; i<StartLevels; i++)
        {
            Layers.Add(MakeLevel(transform.position.y - (DistanceBetweenPlatforms * i)));
        }
        Zombies = new List<GameObject>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        var currentLayer = GetClosestLayer();

        if (Timer > TileDestroyDelay && currentLayer.Count > 1)
        {
             //destroy a tile in the last layer in layers (current layer).
            DestroyRandomTile(currentLayer);
            Timer = 0;
        }
    }

    /// <summary>
    /// Finds the closest layer of platforms to the player.</summary>
    /// <returns>The list that represents the cloest layer</returns>
    private List<GameObject> GetClosestLayer()
    {
        var closestLayer = (layer: Layers[0], distance: Mathf.Abs(Player.transform.position.y - Layers[0][0].transform.position.y));
        foreach (var layer in Layers)
        {
            float distanceFromPlayerToLayer = Mathf.Abs(Player.transform.position.y - layer[0].transform.position.y);
            if (distanceFromPlayerToLayer < closestLayer.distance)
            {
                closestLayer = (layer, distanceFromPlayerToLayer);
            }
        }
        return closestLayer.layer;
    }

    /// <summary>
    /// Creates a new layer of platforms to walk around on.
    /// </summary>
    /// <param name="y"> The y position of the platform.</param>
    /// <returns>A list of the gameobjects that make the platform.</returns>
    private List<GameObject> MakeLevel(float y)
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

    private void DestroyRandomTile(List<GameObject> level)
    {
        int tileIndex = UnityEngine.Random.Range(1, level.Count);

        //pick a random tile from level
        var tileToDestroy = level[tileIndex];

        //Turn tile red
        var rend = tileToDestroy.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.red);

        //Spawn Zombie (1/3 of the time)
        if (tileIndex % 3 == 0 && Zombies.Count < MaxZombies)
        {
            Zombies.Add(Instantiate(ZombiePrefab, tileToDestroy.transform.position + Vector3.up, Quaternion.identity));
        }

        //Destroy tile and remove from level list
        Destroy(tileToDestroy, RedToDestroyTime);
        level.Remove(tileToDestroy);
    }
}