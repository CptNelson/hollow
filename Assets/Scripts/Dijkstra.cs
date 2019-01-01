using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;

public class Dijkstra
{
    //To get a Dijkstra map, you start with an integer array representing your map, 
    //with some set of goal cells set to zero and all the rest set to a very high number. 
    //Iterate through the map's "floor" cells -- skip the impassable wall cells. 
    //If any floor tile has a value greater than 1 regarding to its lowest-value floor neighbour 
    //(in a cardinal direction - i.e. up, down, left or right; a cell next to the one we are checking), 
    //set it to be exactly 1 greater than its lowest value neighbor. Repeat until no changes are made. 
    //The resulting grid of numbers represents the number of steps that it will take to get from any given tile to the nearest goal. 

    private static DataTile _tile;
    private static Dictionary<Vector3, DataTile> _gameTiles;
    private static Entity _player;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
    private static GameObject _map;
    private static List<Entity> _entities;
    private static int _width = GameMaster.width;
    private static int _height = GameMaster.height;

    private int[,] dijkstraMap;
    private Vector3Int goalPosition;


    public Dijkstra()
    {
    }

    public void CreateDijMap(Vector3Int _goalPosition)
    {
        goalPosition = _goalPosition;
        InitDijMap();
        SetGoal();
        SetWeights();
        IterateMap();
    }

    private void InitDijMap()
    {
        dijkstraMap = new int[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //Debug.Log("x: " + x + " y: " + y);
                dijkstraMap[x, y] = 666;
            }
        }
    }

    private void SetGoal()
    {
        dijkstraMap[goalPosition.x, goalPosition.y] = 0;
    }

    // return new Vector3Int(goalPosition.x + i, goalPosition.y);

    private void SetWeights()
    {

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

            Debug.Log("x: " + x + " y: " + y + " dij: " + dijkstraMap[x,y]);
            int[] lowest = new int[4];
                if (x + 1 < _width) lowest[0] = dijkstraMap[x + 1, y];
                if (x - 1 > 0) lowest[1] = dijkstraMap[x - 1, y];
                if (y + 1 < _height) lowest[2] = dijkstraMap[x, y + 1]; 
                if (y - 1 > 0) lowest[3] = dijkstraMap[x, y-1];



                if (lowest.Min() < dijkstraMap[x, y])
                {
                    dijkstraMap[x, y] = lowest.Min() + 1;
                //Debug.Log("x: " + x + " y :" + y + " map: " + dijkstraMap[x, y]);
            }
            }
        }

    }

    private void IterateMap()
    {
        var tiles = TileCollection.instance.tiles;
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (dijkstraMap[x, y] < 9)

                    if (tiles.TryGetValue(new Vector3Int(x, y, 0), out _tile))
                    {
                        _tile.TilemapMember.SetColor(new Vector3Int(x, y, 0), Color.red);
                       //Debug.Log("x: " + x + " y :" +y + " map: " +dijkstraMap[x, y]);
                    }
            }
        }
    }
}



