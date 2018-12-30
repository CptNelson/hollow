using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover {

    private static GameObject map;

    public static void MoveToPosition(Entity _entity, int x, int y)
    {
        map = GameObject.Find("Map");

        Vector3Int oldCellPosition = _entity.Position;
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);

        if (Utils.IsTileEmpty(newCellPosition))
        {
                    // if move is possible, update the entitys grid position and render position.
                    _entity.Position = newCellPosition;
                    _entity.Sprite.GetComponent<Transform>().position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
        }
    }

    public static void MoveToCell(Entity _entity, int x, int y)
    {
        map = GameObject.Find("Map");

        //Vector3Int oldCellPosition = _entity.Position;
        Vector3Int newCellPosition = new Vector3Int(x, y, 0);

        if (Utils.IsTileEmpty(newCellPosition))
        {
            // if move is possible, update the entitys grid position and render position.
            _entity.Position = newCellPosition;
            _entity.Sprite.GetComponent<Transform>().position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
        }
    }
}

