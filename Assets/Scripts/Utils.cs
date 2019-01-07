using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Utils
{
    private static List<Entity> entities;
    private static Tilemap tilemap;

    public static List<Vector3Int> GetNeighbors(Vector3Int position)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        for (int ox = -1; ox < 2; ox++)
        {
            for (int oy = -1; oy < 2; oy++)
            {
                if (ox == 0 && oy == 0)
                    continue;

                neighbors.Add(new Vector3Int(position.x + ox, position.y + oy, 0));
            }
        }

        var tempN = Shuffle(neighbors);
        return tempN;
    }

    public static List<T> Shuffle<T>(List<T> ts)
    {
        int n = ts.Count;
        var last = n - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, n);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
        return ts;
    }





    public static readonly Vector3Int[] DIRS = new[]
    {
            new Vector3Int(1, 0,0),
            new Vector3Int(0, -1,0),
            new Vector3Int(-1, 0,0),
            new Vector3Int(0, 1,0),
            new Vector3Int(1, 1,0),
            new Vector3Int(-1, -1,0),
            new Vector3Int(-1, 1,0),
            new Vector3Int(1, -1,0)
    };
       


    //Return entity in position
    public static Entity IsEntity(Vector3Int position)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i].GetComponent<LivingComponent>().Position == position)
            {
                return entities[i];
            }
        }
        return null;
    }

    public static bool IsTileEmpty(Vector3Int position)
    {
        entities = GameMaster.entitiesList;
        if (TileCollection.instance.tiles[position].IsWalkable)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (IsEntity(position) != null)
                {
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
