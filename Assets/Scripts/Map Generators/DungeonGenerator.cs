using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{

    private static int height = 32;
    private static int width = 70;
    private static Tile[] tileset;
    private static Tilemap tilemap;
    private static GameObject map;

    private static void FillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[1]);
            }
        }
    }

    private static void GenerateEmptyTiles(int amount)
    {
        for (int x = 1; x < width-1; x++){
            for (int y = 1; y < height-1; y++) {
                int r = Utils.GetRandomInt(0, 100);
                if (r < amount)
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[0]);
            }
        }
    }

    private static void GenerateWallTiles(int amount)
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
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
            for (int x = 1; x < width-1; x++)
            {
                for (int y = 1; y < height-1; y++)
                {
                     int grounds = 0;
                        int walls = 0;

                        for (int ox = -1; ox < 2; ox++)
                        {
                            for (int oy = -1; oy < 2; oy++)
                            {
                                if (x + ox < 0 || x + ox >= width || y + oy < 0
                                        || y + oy >= height)
                                    continue;
                                if (tilemap.GetTile(new Vector3Int(x +ox,y +oy,0)).name == "ground0")
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

    
    private void Enclose()
    {
        for (int x = 0; x < width; x++)
        {

        }
    }

    public static void CreateMap()
    {
        FillMap();
        GenerateEmptyTiles(50);
        Smooth(5);
        GenerateEmptyTiles(60);
        Smooth(2);
        GenerateWallTiles(14);
        SetTilesInvisible();

    }

    public static void SetTilesInvisible()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                var color = tilemap.GetColor(new Vector3Int(x, y, 0));
                color.a = 0;
                tilemap.SetColor(new Vector3Int(x, y, 0), color);
            }
        }
    }

    public static void Create()
    {
        map = GameObject.Find("Map");
        tilemap = map.transform.GetChild(0).GetComponent<Tilemap>();
        tileset = TileLoader.LoadTiles("Ascii");
        CreateMap();
    }
}
