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

            string wall = "wall";
            string bush = "bush";
            string ground = "ground";
            string thisTile = Tilemap.GetSprite(localPlace).ToString();
            
            if (thisTile.CompareTo(wall) == 1) 
            {
                var tile = new WorldTile
                {
                    // wall
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 420, 
                    isWalkable = false
                };
                tiles.Add(tile.WorldLocation, tile);
            }

            else if (thisTile.CompareTo(bush) == 1)
            {
                var tile = new WorldTile
                {
                    // bush
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 4,
                    isWalkable = true
                };
                tiles.Add(tile.WorldLocation, tile);
            }
            else if (thisTile.CompareTo(ground) == 1) 
            {
                var tile = new WorldTile
                {
                    // ground
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 0, 
                    isWalkable = true
            };
            tiles.Add(tile.WorldLocation, tile);
            }
            
            
        }
    }

   
}