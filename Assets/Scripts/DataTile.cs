using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//here is data and functions for the 'virtual' tiles. 
public class DataTile : CoreEntity
{
    public Vector3Int LocalPlace { get; set; }

    public Vector3 WorldLocation { get; set; }

    public TileBase TileBase { get; set; }

    public Tilemap TilemapMember { get; set; }

    public string Name { get; set; }

    public string Position { get; set; }

    public bool IsWalkable { get; set; }

    public bool BlocksVision { get; set; }

    //below is needed for pathfinding
    public bool IsExplored { get; set; }

    public DataTile ExploredFrom { get; set; }

    public int Cost { get; set; }
}