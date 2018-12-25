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
    private static int maxDistance = 10;




    //If tile is explored, change color.alpha to 0.5. If it's not, changeg it to 0 so it will be invisible
    private static void CheckIfExplored()
    {
        foreach (KeyValuePair<Vector3, WorldTile> tiles in _gameTiles)
        {
            var color = tiles.Value.TilemapMember.GetColor(tiles.Value.LocalPlace);
            if (!tiles.Value.IsExplored) { color.a = 0.0f; }
            else { color.a = 0.5f; }
            tiles.Value.TilemapMember.SetColor(tiles.Value.LocalPlace, color);
        }
    }


    private static void DoTheFOV()
    {
        for (var row = 1; row < maxDistance; row++)
        {
            for (var col = 0; col <= row; col++)
            {
                var x = _player.Position.x + col;
                var y = _player.Position.y + row;

                var worldPoint = new Vector3Int(x, y, 0);
                if (_gameTiles.TryGetValue(worldPoint, out _tile))
                {
                    var color = _tile.TilemapMember.GetColor(_tile.LocalPlace);
                    color.a = 1.0f;
                    _tile.IsExplored = true;
                    _tile.TilemapMember.SetColor(_tile.LocalPlace, color);
                }

                //show entities
                foreach (Entity entity in entities)
                {

                    if (entity.Position == new Vector3Int(x, y, 0))
                    {
                        entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
    }

    //make all entities(-player) invisible
    private static void HideEntities()
    {
        for (int i = 1; i < entities.Count; i++)
        {
            entities[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public static void DrawOctant()
    {
        entities = GameMaster.entitiesList;
        _player = GameMaster.player;
      
        _gameTiles = GameTiles.instance.tiles;

        HideEntities();
        CheckIfExplored();
        DoTheFOV();
        

    }
}
