using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Geometry : MonoBehaviour
{
    private static WorldTile _tile;
    private static Dictionary<Vector3, WorldTile> _gameTiles;
    private static Entity _player;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
    private static GameObject _map;
    private static List<Entity> entities;

    //tee virtuaatileistä isExplored hommeli

    public static void GreyExplored()
    {
        foreach (KeyValuePair<Vector3, WorldTile> expTiles in GameTiles.instance.tiles)
        {
            if (expTiles.Value.IsExplored)
            {
                var colorExplored = expTiles.Value.TilemapMember.GetColor(expTiles.Value.LocalPlace);
                colorExplored.a = 0.5f;
                expTiles.Value.TilemapMember.SetColor(expTiles.Value.LocalPlace, colorExplored);
            }
            // do something with entry.Value or entry.Key
        }
    }


    public static void DrawOctant()
    {
        entities = GameMaster.entitiesList;
        _player = GameMaster.player;
        int maxDistance = 10;
        
        
        _gameTiles = GameTiles.instance.tiles; 

        foreach(KeyValuePair<Vector3, WorldTile> tiles in GameTiles.instance.tiles)
        {
            var color = tiles.Value.TilemapMember.GetColor(tiles.Value.LocalPlace);
            if (!tiles.Value.IsExplored) { color.a = 0.0f; }
                else { color.a = 0.5f; }
            
            tiles.Value.TilemapMember.SetColor(tiles.Value.LocalPlace, color);
           // Debug.Log(color.a);
        }



        for (int i = 1; i < entities.Count; i++)
        {
            entities[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        for (var row = 1; row < maxDistance; row++) {
            for (var col = 0; col <= row; col++) {
                var x = _player.Position.x + col;
                var y = _player.Position.y + row;

               // var color = _tilemap.GetColor(new Vector3Int(x, y, 0));
               // color.a = 1;
               // _tilemap.SetColor(new Vector3Int(x, y, 0), color);

               // _gameTiles = GameTiles.instance.tiles; // This is our Dictionary of tiles
                var worldPoint = new Vector3Int(x, y, 0);
                if (_gameTiles.TryGetValue(worldPoint, out _tile))
                {
                    var color = _tile.TilemapMember.GetColor(_tile.LocalPlace);
                    color.a = 1.0f;
                    _tile.IsExplored = true;
                    _tile.TilemapMember.SetColor(_tile.LocalPlace, color);
                }

                foreach (Entity entity in entities)
                {

                    //entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                    if (entity.Position == new Vector3Int(x,y, 0))
                    {
                        entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
       // GreyExplored();
    }
}
