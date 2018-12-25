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

    public static bool IsTileEmpty(Vector3Int position)
    {
        entities = GameMaster.entitiesList;
        if (GameTiles.instance.tiles[position].isWalkable)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Position == position)
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
        } while (!IsTileEmpty(position));
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
