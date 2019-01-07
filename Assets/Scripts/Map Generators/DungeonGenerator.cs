using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    private static Tile[] _tileset;
    private static Tilemap _tilemap;
    private static GameObject _map;
    private static int _width;
    private static int _height;
    private static int[,] regions;
    private static int nextRegion;

    public static void Create(int width, int height)
    {
        _width = width;
        _height = height;
        _map = GameObject.Find("Map");
        _tilemap = _map.transform.GetChild(0).GetComponent<Tilemap>();
        _tileset = TileLoader.LoadTiles("Ascii");
        CreateMap();

    }



    private static void CreateMap()
    {
        FillMap();
        GenerateEmptyTiles(50);
        Smooth(15);
        
        CreateRegions();
      //  GenerateEmptyTiles(60);
      //  Smooth(2);
      //  GenerateBushTiles(4);
        //Set tiles invisible so player doesn't see the whole map
        SetTilesInvisible();
    }

    private static void Connect()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_tilemap.GetTile(new Vector3Int(x, y, 0)).name == "00ground0")
                {

                }
            }
        }
    }

    //Fill map with wall tiles
    private static void FillMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
            }
        }
    }

    private static void CreateRegions()
    {
        nextRegion = 1;
        regions = new int[_width, _height];



        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_tilemap.GetTile(new Vector3Int(x,y,0)).name != "01wall0" && regions[x,y] == 0)
                {

                    int size = fillRegion(nextRegion++, x, y);
                   // return;
                    if (size < 46)
                        removeRegion(nextRegion - 1, 0);
                }
            }
        }
    }

    private static int fillRegion(int region, int x, int y)
    {
        int size = 1;
        List<Vector3Int> open = new List<Vector3Int>();
        open.Add(new Vector3Int(x, y, 0));
        regions[x,y] = region;

        while (open.Count > 0 )
        {
           // Debug.Log("asd1: " + open.Count + " test: " + test);
            Vector3Int p = open[0];
            open.RemoveAt(0);

            List<Vector3Int> neighbors = Utils.GetNeighbors(p);

            foreach(Vector3Int n in neighbors)
            {
                if (n.x < 0 || n.y < 0 || n.x >= _width || n.y >= _height)
                    continue;   

                if (regions[n.x, n.y] > 0 || _tilemap.GetTile(new Vector3Int(n.x, n.y, 0)).name == "01wall0") {

                    continue;
                }

                size++;
                regions[n.x, n.y] = region;
                open.Add(n);

            }

        }
        Debug.Log("open: " + open.Count);
        return size;
    }

    private static void removeRegion(int region, int z)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (regions[x,y] == region)
                {
                    regions[x,y] = 0;
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
                }
            }
        }
    }

    //add random ground tiles but leave walls on perimeters
    private static void GenerateEmptyTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[0]);
            }
        }
    }

    //create some random bushes
    private static void GenerateBushTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[2]);
            }
        }
    }

    // create some random wall tiles
    private static void GenerateWallTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
            }
        }
    }

    // Cellular Automata
    // smooth over the map and turn grounds to walls and walls to grounds according to the rules
    private static void Smooth(int times)
    {
        Tilemap tempTiles = _tilemap;
        for (int time = 0; time < times; time++)
        {
            for (int x = 1; x < _width - 1; x++)
            {
                for (int y = 1; y < _height - 1; y++)
                {
                    int grounds = 0;
                    int walls = 0;

                    for (int ox = -1; ox < 2; ox++)
                    {
                        for (int oy = -1; oy < 2; oy++)
                        {
                            if (x + ox < 1 || x + ox > _width || y + oy < 1
                                    || y + oy > _height)
                                continue;
                            if (_tilemap.GetTile(new Vector3Int(x + ox, y + oy, 0)).name == "00ground0")
                                grounds++;
                            else
                                walls++;
                        }
                    }

                    if (grounds > walls)
                    {
                        tempTiles.SetTile(new Vector3Int(x, y, 0), _tileset[0]);
                    }
                    else if (walls > grounds)
                        tempTiles.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
                }
            }
        }
        _tilemap = tempTiles;
    }


    public static void SetTilesInvisible()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                var color = _tilemap.GetColor(new Vector3Int(x, y, 0));
                color.a = 0;
                _tilemap.SetColor(new Vector3Int(x, y, 0), color);

            }
        }
    }


}
