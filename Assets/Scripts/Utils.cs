using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class Utils
{
    private static List<Entity> entities;
    private static Tilemap tilemap;
    //private static GameTiles tiles;
    private static System.Random random;

    public static bool IsTileEmpty(Vector3Int position)//Vector3Int position)
    {
        entities = GameMaster.entitiesList;
        if (GameTiles.instance.tiles[position].isWalkable)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].position == position)
                {
                    Debug.Log("there is someone!");
                    return false;
                }
                
            } return true;
        }
        else return false;
    }
    
    public static Vector3Int GetRandomEmptyPosition()
    {
        tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();

        random = new System.Random();
        Vector3Int position;

        do {
            position = new Vector3Int(random.Next(0, tilemap.size.x), random.Next(0, tilemap.size.y), 0);
        } while (!IsTileEmpty(position));


        return position;

    }

}
