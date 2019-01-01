using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTiles : MonoBehaviour
{
    private DataTile _tile;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var worldPoint = new Vector3Int((int)point.x, (int)point.y, 0);
            
            var tiles = TileCollection.instance.tiles; // This is our Dictionary of tiles

            if (tiles.TryGetValue(worldPoint, out _tile))
            {
                print("Tile " + _tile.Name + " walkable: " + _tile.IsWalkable + " position: " + _tile.Position);
                _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {

            
            var worldPoint = new Vector3Int(1, 1, 0);

            var tiles = TileCollection.instance.tiles; // This is our Dictionary of tiles

            if (tiles.TryGetValue(worldPoint, out _tile))
            {
                print("Tile " + _tile.Name + " costs: " + _tile.Cost + " position: " + _tile.Position);
                _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
            }
        }
    }
}
