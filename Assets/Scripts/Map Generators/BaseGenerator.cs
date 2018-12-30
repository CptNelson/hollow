using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseGenerator
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
                _tilemap.SetTile(new Vector3Int(x, y, 0), _tileset[1]);
            }
        }
    }


    public static void CreateMap()
    {
        FillMap();
    }

    public static void Create(int width, int height)
    {
        _width = width;
        _height = height;
        _map = GameObject.Find("Map");
        _tilemap = _map.transform.GetChild(0).GetComponent<Tilemap>();
        _tileset = TileLoader.LoadTiles("Ascii");
        CreateMap();
    }

}
