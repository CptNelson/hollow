﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FOV 
{
    private static WorldTile _tile;
    private static Dictionary<Vector3, WorldTile> _gameTiles;
    private static Entity _entity;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
    private static GameObject _map;
    private static List<Entity> _entities;
    private static int _maxDistance;
    private static Vector3Int _worldPoint;
    private static List<Entity> entitiesInFOV;

    public static void UpdatePlayerFOV()
    {
        _entities = GameMaster.entitiesList;
        _gameTiles = GameTiles.instance.tiles;
        _entity = GameMaster.player;
        _maxDistance = 12;
        Debug.Log("Update: P: " + _entity.Name);

        HideEntities();
        CheckIfExplored();
        DoTheFOV();
    }

    public static List<Entity> UpdateEntityFOV(Entity entity, int distance)
    {
        entitiesInFOV = new List<Entity>();
        _entities = GameMaster.entitiesList;
        _gameTiles = GameTiles.instance.tiles;
        _entity = entity;
        _maxDistance = 10;
        Debug.Log("Update: P: " + _entity.Name);

        GetEntityFOV();

        return entitiesInFOV;

    }



    //If tile is explored, change color.alpha to 0.5. If it's not, change it to 0 so it will be invisible
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
        int row, col;

        // iterate to make a full square from octants
        for (int octant = 0; octant < 8; octant++)
        {
            for (row = 1; row < _maxDistance; row++)
            {
                for (col = 0; col <= row; col++)
                {

                    // GetPosition gets different positions depending on what octant iteration we are at.
                    var worldPoint = GetPosition(octant, row, col);

                    //change explored status of tiles and colors for tiles that are inside FOV.
                    if (_gameTiles.TryGetValue(worldPoint, out _tile))
                    {

                        var color = _tile.TilemapMember.GetColor(_tile.LocalPlace);
                        color.a = 1.0f;
                        _tile.IsExplored = true;

                        _tile.TilemapMember.SetColor(_tile.LocalPlace, color);

                    }


                    //show entities that are inside FOV.
                    foreach (Entity entity in _entities)
                    {

                        if (entity.Position == worldPoint)
                        {
                            entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                }
            }
        }
    }

    private static void GetEntityFOV()
    {
        int row, col;

        // iterate to make a full square from octants
        for (int octant = 0; octant < 8; octant++)
        {
            for (row = 1; row < _maxDistance; row++)
            {
                for (col = 0; col <= row; col++)
                {

                    // GetPosition gets different positions depending on what octant iteration we are at.
                    var worldPoint = GetPosition(octant, row, col);


                    //show entities that are inside FOV.
                    foreach (Entity entity in _entities)
                    {

                        if (entity.Position == worldPoint)
                        {
                            entitiesInFOV.Add(entity);
                        }
                    }
                }
            }
        }
    }

    private static Vector3Int GetPosition(int octant, int row, int col)
    {
        //Debug.Log(_entity.Name +" "+ _entity.Position);
        switch (octant)
        {
            case 0:
                return new Vector3Int(_entity.Position.x + col, _entity.Position.y + row, 0);
            case 1:
                return new Vector3Int(_entity.Position.x - col, _entity.Position.y - row, 0);
            case 2:
                return new Vector3Int(_entity.Position.x - col, _entity.Position.y + row, 0);
            case 3:
                return new Vector3Int(_entity.Position.x + col, _entity.Position.y - row, 0);
            case 4:
                return new Vector3Int(_entity.Position.x + row, _entity.Position.y + col, 0);
            case 5:
                return new Vector3Int(_entity.Position.x - row, _entity.Position.y - col, 0);
            case 6:
                return new Vector3Int(_entity.Position.x + row, _entity.Position.y - col, 0);
            case 7:
                return new Vector3Int(_entity.Position.x - row, _entity.Position.y + col, 0);
        }
        return new Vector3Int(_entity.Position.x - row, _entity.Position.y + col, 0);
    }



    private static void ShowFOV()
    {
        Debug.Log("Show FOV: " + _worldPoint);
        if (_gameTiles.TryGetValue(_worldPoint, out _tile))
        {
            Debug.Log("Change Tile: ");
            var color = _tile.TilemapMember.GetColor(_tile.LocalPlace);
            color.a = 1.0f;
            _tile.IsExplored = true;

            _tile.TilemapMember.SetColor(_tile.LocalPlace, color);
        }

        foreach (Entity entity in _entities)
        {

            if (entity.Position == _worldPoint)
            {
                entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

    }

    //make all entities except player invisible
    private static void HideEntities()
    {
        Debug.Log("Hide Entities");
        for (int i = 1; i < _entities.Count; i++)
        {
            _entities[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

   