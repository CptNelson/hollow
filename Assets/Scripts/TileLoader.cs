using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLoader : ScriptableObject {

    public static Tile[] LoadTiles(string name)
    {
        return Resources.LoadAll<Tile>("Tilesets/" + name);        
    }
}
