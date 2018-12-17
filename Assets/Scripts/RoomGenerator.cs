using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : ScriptableObject {


    private static Tile[] tiles;
    private static Tilemap[] layers;
    private static GameObject map;
    private static GridLayout gridLayout;

    private static void LoadTiles(string name)
    {
        tiles = Resources.LoadAll<Tile>("Tilesets/" + name);
    }

    private static void GenerateRoom()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                layers[0].SetTile(new Vector3Int(x, y, 0), tiles[1]);
            }
        }

        for (int x = 1; x < 19; x++)
        {
            for (int y = 1; y < 7; y++)
            {
                layers[0].SetTile(new Vector3Int(x, y, 0), tiles[0]);
            }
        }
    }

    public static void CreateRoom() {

        map = GameObject.Find("Map");
        gridLayout = map.GetComponent<Grid>();
        layers = new Tilemap[3];

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = map.transform.GetChild(i).GetComponent<Tilemap>();
        }

        LoadTiles("Cave");
        GenerateRoom();
    }
	
}
