using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover {

    private static WorldTile _tile;
    private static GameObject map;
    private static List<Entity> entitiesList;


    public static void MoveToPosition(int x, int y, Entity entity)
    {
        map = GameObject.Find("Map");
        entitiesList = GameMaster.entitiesList;

        Vector3Int oldCellPosition = entity.position;
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);


            if (GameTiles.instance.tiles.TryGetValue(newCellPosition, out _tile))
            {
                if (_tile.isWalkable)
                {
                    //iterate all entitites and check if someone is already at target tile.
                    for (int i = 0; i < entitiesList.Count; i++)
                    {
                        if (entitiesList[i].position == newCellPosition)
                        {
                        Debug.Log("there is someone!");
                        return;
                        }
                    }
                // if move is possible, update the entitys grid position and render position.
                entity.position = newCellPosition;
                entity.sprite.GetComponent<Transform>().position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
            }
            else Debug.Log("you can't go there!");
        }

        
    }
}

