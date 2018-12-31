using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Utils
{
    private static List<Entity> entities;
    private static Tilemap tilemap;
    //private static GameTiles tiles;

    //private static object syncObj = new object();

    public static readonly Vector3Int[] DIRS = new[]
    {
            new Vector3Int(1, 0,0),
            new Vector3Int(0, -1,0),
            new Vector3Int(-1, 0,0),
            new Vector3Int(0, 1,0)
        };

    public static Entity IsEntity(Vector3Int position)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i].Position == position)
            {
                return entities[i];
            }
        }
        return null;
    }

    public static bool Is2TileEmpty(Vector3Int position)
    {
        entities = GameMaster.entitiesList;
        if (GameTiles.instance.tiles[position].isWalkable)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (IsEntity(position) != null)//entities[i].Position == position)
                {

                    //Debug.Log("there is someone!");
                    return false;
                }
            } return true;
        }
        else return false;
    }
    
    public static Vector3Int GetRandomEmptyPosition()
    {
        tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
        System.Random rand = new System.Random();

        Vector3Int position;

        do {
            position = new Vector3Int(rand.Next(0, tilemap.size.x), rand.Next(0, tilemap.size.y), 0);
        } while (!Is2TileEmpty(position));
        return position;
    }

    public static int GenerateRandomInt(int min, int max)
    {
        System.Random rand = new System.Random();
        return rand.Next(min, max);       
    }


    //This might be too heavy for many random numbers at same time
    public static int GetRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

}
