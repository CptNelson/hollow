﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ForestGenerator 
{

    private static Tile[] _tileset;
    private static Tilemap _tilemap;
    private static GameObject _map;
    private static int _width;
    private static int _height;

    private static void FillMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[2]);
            }
        }
    }

    private static void GenerateTreeTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[3]);
                else if (r > amount && r < 65) _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[4]);
                else _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[0]);
            }
        }
    }

    private static void GenerateGrassTiles(int amount)
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
                            if (x + ox < 0 || x + ox >= _width || y + oy < 0
                                    || y + oy >= _height)
                                continue;
                            if (_tilemap.GetTile(new Vector3Int(x + ox, y + oy, 0)).name == "bush0")
                                grounds++;
                            else
                                walls++;
                        }
                    }
                    if (grounds >= walls)
                    {
                        tempTiles.SetTile(new Vector3Int(x, y, 0), _tileset[0]);
                    }
                    else
                        tempTiles.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
                }
            }
        }
        _tilemap = tempTiles;
    }




    public static void CreateMap()
    {
        FillMap();
        GenerateTreeTiles(25);
        Smooth(0);
        GenerateGrassTiles(30);
        Smooth(1);
        SetTilesInvisible();

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

    public static void Create(int width, int height)
    {
        _width = width;
        _height = height;
        _map = GameObject.Find("Map");
        _tilemap = _map.transform.GetChild(0).GetComponent<Tilemap>();
        _tileset = TileLoader.LoadTiles("Ascii/Forest/");
        CreateMap();
    }
}
