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

    public Vector3Int Position { get; set; }

    public bool IsWalkable { get; set; }

    public bool IsTransparent { get; set; }

    //below is needed for pathfinding
    public bool IsExplored { get; set; }

    public DataTile ExploredFrom { get; set; }

    public int Cost { get; set; }

    public bool IsVisible { get { return _isVisible; }}
    private bool _isVisible;

    public bool SetIsVisible(bool isVisible)
    {
        if (_isVisible != isVisible)
        {
            _isVisible = isVisible;

            // visibility changed
            return true;
        }
        else
        {
            // no change
            return false;
        }
    }

    public void SetTileVisibility(float alpha) 
    {
            var color = this.TilemapMember.GetColor(this.Position);
            color.a = alpha;
            this.TilemapMember.SetColor(this.Position, color);
    }

}