﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCollection : MonoBehaviour
{
    public static TileCollection instance;

    public Tilemap Tilemap;

    public Dictionary<Vector3, DataTile> tiles;


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
        tiles = new Dictionary<Vector3, DataTile>();
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
                var tile = new DataTile
                {
                    // wall
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 420,
                    IsWalkable = false,
                    HP = 666,
                    BlocksVision = true
                };
                tiles.Add(tile.WorldLocation, tile);
            }

            else if (thisTile.CompareTo(bush) == 1)
            {
                var tile = new DataTile
                {
                    // bush
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 4,
                    HP = 30,
                    IsWalkable = true,
                    BlocksVision = true
                };
                tiles.Add(tile.WorldLocation, tile);
            }
            else if (thisTile.CompareTo(ground) == 1) 
            {
                var tile = new DataTile
                {
                    // ground
                    LocalPlace = localPlace,
                    WorldLocation = Tilemap.CellToWorld(localPlace),
                    TileBase = Tilemap.GetTile(localPlace),
                    TilemapMember = Tilemap,
                    Name = Tilemap.GetSprite(localPlace).ToString(),
                    Position = localPlace.x + "," + localPlace.y,
                    Cost = 0,
                    BlocksVision = false,
                    IsWalkable = true
                    
            };
            tiles.Add(tile.WorldLocation, tile);
            }
            
            
        }
    }

   
}