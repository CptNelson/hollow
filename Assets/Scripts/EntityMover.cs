using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover : ScriptableObject {

    private static WorldTile _tile;
    private static GameObject map = GameObject.Find("Map");


    public static void MoveToPosition(int x, int y, GameObject entity)
    {
        Vector3Int oldCellPosition = map.GetComponent<GridLayout>().LocalToCell(entity.transform.localPosition);
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);


            if (GameTiles.instance.tiles.TryGetValue(newCellPosition, out _tile))
            {
                if (_tile.isWalkable)
                {
                    entity.transform.position = map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
                }
            }

        
    }
}

