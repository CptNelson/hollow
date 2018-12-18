using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover {

    private static GameObject map;

    public static void MoveToPosition(int x, int y, Entity entity)
    {
        map = GameObject.Find("Map");

        Vector3Int oldCellPosition = entity.position;
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);

        if (Utils.IsTileEmpty(newCellPosition))
        {
                    // if move is possible, update the entitys grid position and render position.
                    entity.position = newCellPosition;
                    entity.sprite.GetComponent<Transform>().position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
        }
    }
}

