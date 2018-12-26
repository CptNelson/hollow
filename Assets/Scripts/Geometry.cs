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

    //new Vector3Int(_player.Position.x + col, _player.Position.y + row, 0);

    private static void GetPosition(int octant, int row, int col, out int X, out int Y)
    {
        X = Y = 0;
        switch (octant)
        {
            case 0:
                X = _player.Position.x + col;
                Y = _player.Position.y + row;
                break;
            case 1:
                X = _player.Position.x - col;
                Y = _player.Position.y - row;
                break;
            case 2:
                X = _player.Position.x + col;
                Y = _player.Position.y - row;
                break;
            case 3:
                X = _player.Position.x - col;
                Y = _player.Position.y + row;
                break;
            case 4:
                X = _player.Position.x + row;
                Y = _player.Position.y + col;
                break;
            case 5:
                X = _player.Position.x - row;
                Y = _player.Position.y - col;
                break;
            case 6:
                X = _player.Position.x + row;
                Y = _player.Position.y - col;
                break;
            case 7:
                X = _player.Position.x - row;
                Y = _player.Position.y + col;
                break;
        }
    }

    private static void DoTheFOV(int octant)
    {
        int row;
        int col;
        int X;
        int Y;
        for (row = 1; row < maxDistance; row++) {
            for (col = 0; col <= row; col++) {

                // GetPosition gets different positions depending on what octant iteration we are at.
                GetPosition(octant, row, col, out X, out Y);

                var worldPoint = new Vector3Int(X, Y, 0);
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

                    if (entity.Position == new Vector3Int(X, Y, 0))
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

        // iterate to make a full square from octants
        for (int octant = 0; octant < 8; octant++)
        {
            DoTheFOV(octant);
        }
        




    }
}