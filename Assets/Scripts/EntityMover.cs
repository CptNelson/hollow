using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover : ScriptableObject {

    private static WorldTile _tile;
    private static GameObject map = GameObject.Find("Map");
    private static List<Entity> entitiesList;


    public static void MoveToPosition(int x, int y, GameObject entity)
    {
        entitiesList = EntitySpawner.EntitiesList;

        Vector3Int oldCellPosition = map.GetComponent<GridLayout>().LocalToCell(entity.transform.localPosition);
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);


            if (GameTiles.instance.tiles.TryGetValue(newCellPosition, out _tile))
            {
                if (_tile.isWalkable)
                {
                    for (int i = 0; i < entitiesList.Count; i++)
                    {
                        if (entitiesList[i].position == newCellPosition)
                        {
                            entity.transform.position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
                        
                        }
                        else Debug.Log("there is someone!");
                    }                    
                }
            else Debug.Log("you can't go there!");
        }

        
    }
}

