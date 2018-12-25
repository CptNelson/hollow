using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Geometry : MonoBehaviour
{
    private static WorldTile _tile;
    private static Entity _player;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
    private static GameObject _map;
    private static List<Entity> entities;


    //entities = GameMaster.entitiesList;
    //    if (GameTiles.instance.tiles[position].isWalkable)
    //    {
    //        for (int i = 0; i<entities.Count; i++)
    //        {
    //            if (entities[i].Position == position)
    //            {
    //                //Debug.Log("there is someone!");
    //                return false;
    //            }
    //        } return true;
    //    }




    public static void DrawOctant()
    {
        entities = GameMaster.entitiesList;
        _player = GameMaster.player;
        int maxDistance = 10;

        for (int i = 1; i < entities.Count; i++)
        {
            entities[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        for (var row = 1; row < maxDistance; row++) {
            for (var col = 0; col <= row; col++) {
                var x = _player.Position.x + col;
                var y = _player.Position.y + row;
                
                foreach(Entity entity in entities)
                {
                    //entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                    if (entity.Position == new Vector3Int(x,y, 0))
                    {
                        entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
    }
}
