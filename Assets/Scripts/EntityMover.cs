using UnityEngine;

public class EntityMover : ScriptableObject {

    private static GameObject map = GameObject.Find("Map");
    private static float oldX, oldY;
    private static Transform target;

    public static void MoveToPosition(int x, int y, GameObject entity)
    {
        Vector3Int oldCellPosition = map.GetComponent<GridLayout>().LocalToCell(entity.transform.localPosition);

        
        
        entity.transform.position = map.GetComponent<Grid>().GetCellCenterLocal(new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0));
    }
}

