using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator
{

    private static Tile[] tileset;
    private static Tilemap tilemap;
    private static GameObject map;

    private static void GenerateRoom()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[1]);
            }
        }

        for (int x = 1; x < 19; x++)
        {
            for (int y = 1; y < 7; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset[0]);
            }
        }
    }

    public static void CreateRoom()
    {
        map = GameObject.Find("Map");
        tilemap = map.transform.GetChild(0).GetComponent<Tilemap>();
        tileset = TileLoader.LoadTiles("Cave");
        GenerateRoom();
    }
}
