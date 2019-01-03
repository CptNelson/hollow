using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shadowcast
{
    private static DataTile _tile;
    private static Dictionary<Vector3, DataTile> _gameTiles;
    private static Entity _entity;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();
    private static GameObject _map;
    private static List<Entity> _entities;
    private static int _maxDistance;
    private static Vector3Int _worldPoint;
    private static List<Entity> entitiesInFOV;


    public static void UpdatePlayerFOV()
    {
        _shadows = new List<Shadow>();
        _entities = GameMaster.entitiesList;
        _gameTiles = TileCollection.instance.tiles;
        _entity = GameMaster.player;
        _maxDistance = 8;
        HideEntities();
        CheckIfExplored();
        DoTheFOV();
    }

    public static List<Entity> UpdateEntityFOV(Entity entity, int distance)
    {
        entitiesInFOV = new List<Entity>();
        entitiesInFOV.Clear();
        _entities = GameMaster.entitiesList;
        _gameTiles = TileCollection.instance.tiles;
        _entity = entity;
        _maxDistance = distance;

        GetEntityFOV();
        return entitiesInFOV;
    }



    //If tile is explored, change color.alpha to 0.5. If it's not, change it to 0 so it will be invisible
    private static void CheckIfExplored()
    {
        foreach (KeyValuePair<Vector3, DataTile> tiles in _gameTiles)
        {
            var color = tiles.Value.TilemapMember.GetColor(tiles.Value.LocalPlace);
            if (!tiles.Value.IsExplored) { color.a = 0.0f; }
            else { color.a = 0.5f; }
            tiles.Value.TilemapMember.SetColor(tiles.Value.LocalPlace, color);
        }
    }

    //This is for the player
    private static void DoTheFOV()
    {

        int row, col = 0 ;

        // iterate to make a full square from octants
        for (int octant = 0; octant < 8; octant++)
        {

            _shadows.Clear();
            bool fullShadow = false;

            for (row = 1; row < _maxDistance; row++)
            {
                var position = GetPosition(octant, row, col);

                for (col = 0; col <= row; col++)
                {
                    bool blocksLight = false;
                    bool isVisible = false;
                    Shadow projection = null;


                   // Debug.Log("row: " + row);
                    // GetPosition gets different positions depending on what octant iteration we are at.


                    //change explored & visibility for tiles and entitites that are inside FOV.
                    if (_gameTiles.TryGetValue(position, out _tile))
                    {
                        if (!fullShadow)
                        {
                            blocksLight = !_tile.IsTransparent;
                            projection = GetProjection(col, row);
                            isVisible = !IsInShadow(projection); 
                    }

                        _tile.SetIsVisible(isVisible);
                        _tile.IsExplored = true;
                        foreach (Entity entity in _entities)
                        {
                            if (entity.Position == position)
                            {
                                entity.Sprite.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }



                        if (blocksLight)
                        {
                            fullShadow = AddShadow(projection);
                        }
                        position += new Vector3Int(1, 0, 0);
                    }
                }
            }
        }
    }

    public static List<Shadow> _shadows;






    public class Shadow
    {
        public float Start { get { return _start; } }
        public float End { get { return _end; } }

        private float _start, _end;

        public Shadow(float start, float end)
        {
            _start = start;
            _end = end;
        }

        public bool Contains(Shadow projection)
        {
            return (_start <= projection.Start) && (_end >= projection.End);
        }

        public void Unify(float start, float end)
        {
            // see if the shadow overlaps to the right
            if (start <= _end)
            {
                _end = Math.Max(_end, end);
            }

            // see if the shadow overlaps to the left
            if (_start <= end)
            {
                _start = Math.Min(_start, start);
            }
        }

    }

    private static Shadow GetProjection(int col, int row)
    {
        // the bottom edge of row 0 is 1 wide
        float rowBottomWidth = row + 1;

        // the top edge of row 0 is 2 wide
        float rowTopWidth = row + 2;

        // unify the bottom and top edges of the tile
        float start = Math.Min(col / rowBottomWidth, col / rowTopWidth);
        float end = Math.Max((col + 1.0f) / rowBottomWidth, (col + 1.0f) / rowTopWidth);

        return new Shadow(start, end);
    }

    private static bool IsInShadow(Shadow projection)
    {
        // optimization note: doing an explicit foreach here is
        // faster than _shadows.Any((shadow) => shadow.Contains(projection));

        // check the shadow list
        foreach (var shadow in _shadows)
        {
            if (shadow.Contains(projection)) return true;
        }

        return false;
    }

    private static bool AddShadow(Shadow shadow)
    {
        int index = 0;
        for (index = 0; index < _shadows.Count; index++)
        {
            // see if we are at the insertion point for this shadow
            if (_shadows[index].Start > shadow.Start)
            {
                // break out and handle inserting below
                break;
            }
        }

        // the new shadow is going here. see if it overlaps the previous or next shadow
        bool overlapsPrev = false;
        bool overlapsNext = false;

        if ((index > 0) && (_shadows[index - 1].End > shadow.Start))
        {
            overlapsPrev = true;
        }

        if ((index < _shadows.Count) && (_shadows[index].Start < shadow.End))
        {
            overlapsNext = true;
        }

        // insert and unify with overlapping shadows
        if (overlapsNext)
        {
            if (overlapsPrev)
            {
                // overlaps both, so unify one and delete the other
                _shadows[index - 1].Unify(shadow.Start, _shadows[index].End);
                _shadows.RemoveAt(index);
            }
            else
            {
                // just overlaps the next shadow, so unify it with that
                _shadows[index].Unify(shadow.Start, shadow.End);
            }
        }
        else
        {
            if (overlapsPrev)
            {
                // just overlaps the previous shadow, so unify it with that
                _shadows[index - 1].Unify(shadow.Start, shadow.End);
            }
            else
            {
                // does not overlap anything, so insert
                _shadows.Insert(index, shadow);
            }
        }

        // see if we are now shadowing everything
        return (_shadows.Count == 1) && (_shadows[0].Start == 0) && (_shadows[0].End == 1.0f);
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


    //make all entities except player invisible
    private static void HideEntities()
    {
        for (int i = 1; i < _entities.Count; i++)
        {
            _entities[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }



}




