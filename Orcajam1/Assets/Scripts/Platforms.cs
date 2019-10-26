using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject TilePrefab;
    
    /// <summary>
    /// 1/2 * the # of tiles on a side.</summary>
    [SerializeField] private int Size = 6;

    /// <summary>
    /// Max # of layers before letting player fall to their doom. </summary>
    [SerializeField] private int MaxLevels = 10;

    [SerializeField] private int DistanceBetweenPlatforms = 5;

    /// <summary>
    /// How long a tile stays red before being destroyed.</summary>
    [SerializeField] private float RedToDestroyTime = 3;

    /// <summary>
    /// Delay before choosing a new tile to destroy.</summary>
    [SerializeField] private float TileDestroyDelay = 0.5f;

    private Transform Player;

    private List<List<GameObject>> Layers;

    private float Timer;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Layers = new List<List<GameObject>>
        {
            //create first layer.
            MakeLevel(0)
        };
    }

    private void Update()
    {
        //get height of parent block in current platform.
        float currentLevelHeight = Layers[Layers.Count - 1][0].transform.position.y;

        //If player goes below the current platform, and we are below maxtiles, make a new platform.
        if (Player.position.y < currentLevelHeight && Layers.Count < MaxLevels)
        {
            Layers.Add(MakeLevel((int)currentLevelHeight - DistanceBetweenPlatforms));
        }
        else 
        {
            //Increment timer, and check if a new tile should be destroyed.
            Timer += Time.deltaTime;
            if (Timer > TileDestroyDelay)
            {
                //destroy a tile in the last layer in layers (current layer).
                DestroyRandomTile(Layers[Layers.Count - 1]);
                Timer = 0;
            }
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

    private void DestroyRandomTile(List<GameObject> level)
    {
        //pick a random tile from level
        var tileToDestroy = level[Random.Range(1, level.Count)];

        //Turn tile red
        var renderer = tileToDestroy.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);

        //Destroy tile and remove from level list
        Destroy(tileToDestroy, RedToDestroyTime);
        level.Remove(tileToDestroy);
    }
}


