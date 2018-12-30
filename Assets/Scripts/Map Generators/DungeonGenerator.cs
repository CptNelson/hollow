using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{


    private static Tile[] tileset;
    private static Tilemap tilemap;
    private static GameObject map;
    private static int _width;
    private static int _height;

    private static void FillMap()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[1]);
            }
        }
    }

    private static void GenerateEmptyTiles(int amount)
    {
        for (int x = 1; x < _width-1; x++){
            for (int y = 1; y < _height-1; y++) {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[0]);
            }
        }
    }


    private static void GenerateBushTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileset[2]);
            }
        }
    }

    private static void GenerateWallTiles(int amount)
    {
        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                    tilemap.SetTile(new Vector3Int(x, y, 0), tileset[1]);
            }
        }
    }


    private  static void Smooth(int times)
    {
        Tilemap tempTiles = tilemap;
        for (int time = 0; time < times; time++)
        {
            for (int x = 1; x < _width-1; x++)
            {
                for (int y = 1; y < _height-1; y++)
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
                                if (tilemap.GetTile(new Vector3Int(x +ox,y +oy,0)).name == "00ground0")
                                    grounds++;
                                else
                                    walls++;
                            }
                        }
                    if (grounds >= walls)
                    {
                        tempTiles.SetTile(new Vector3Int(x, y, 0), tileset[0]);
                    } else
                        tempTiles.SetTile(new Vector3Int(x, y, 0), tileset[1]);
                }
                }
            }
        tilemap = tempTiles;
        }

    


    public static void CreateMap()
    {
        FillMap();
        GenerateEmptyTiles(50);
        Smooth(5);
        GenerateEmptyTiles(60);
        Smooth(2);
        GenerateBushTiles(4);
        SetTilesInvisible();

    }

    public static void SetTilesInvisible()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                var color = tilemap.GetColor(new Vector3Int(x, y, 0));
                color.a = 0;
                tilemap.SetColor(new Vector3Int(x, y, 0), color);
            }
        }
    }

    public static void Create(int width, int height)
    {
        _width = width;
        _height = height;
        map = GameObject.Find("Map");
        tilemap = map.transform.GetChild(0).GetComponent<Tilemap>();
        tileset = TileLoader.LoadTiles("Ascii");
        CreateMap();
    }
}
