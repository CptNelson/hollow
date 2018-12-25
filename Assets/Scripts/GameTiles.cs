using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameTiles : MonoBehaviour
{
    public static GameTiles instance;

    public Tilemap Tilemap;
    //public static GameObject _map;
    //public static Tilemap _tempTilemap;



    public Dictionary<Vector3, WorldTile> tiles;


    public void CreateTileDictionary()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GetWorldTiles();
    }



    // Use this for initialization
    private void GetWorldTiles()
    {
        tiles = new Dictionary<Vector3, WorldTile>();
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(localPlace)) continue;

            string wall = "hash";
            string thisTile = Tilemap.GetSprite(localPlace).ToString();
            
            if (thisTile.CompareTo(wall) == 1) 
            {
                var tile = new WorldTile
                {
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 10, // TODO: Change this with the proper cost from ruletile
                    isWalkable = false
                };
                tiles.Add(tile.WorldLocation, tile);
            } else {
                var tile = new WorldTile
                {
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 1, // TODO: Change this with the proper cost from ruletile
                    isWalkable = true
            };
            tiles.Add(tile.WorldLocation, tile);
            }
            
            
        }
    }

   
}