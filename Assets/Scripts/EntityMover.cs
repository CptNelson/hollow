using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityMover : MonoBehaviour {

    private static GameObject _map;

    //TODO: Combine both move methods.
    //TODO: Move attacking out of here!

    public static void MoveToPosition(Entity _entity, int x, int y)
    {
        _map = GameObject.Find("Map");

        Vector3Int oldCellPosition = _entity.Position;
        Vector3Int newCellPosition = new Vector3Int(oldCellPosition.x + x, oldCellPosition.y + y, 0);
        

        if (Utils.IsTileEmpty(newCellPosition) == true)
        {
            // if move is possible, update the entitys grid position and render position.
            _entity.Position = newCellPosition;
            _entity.Sprite.GetComponent<Transform>().position = _map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
        }
        else if (!Utils.IsTileEmpty(newCellPosition) && Utils.IsEntity(newCellPosition) != null)
        {
            Debug.Log("Attack!");
            Utils.IsEntity(newCellPosition).HP = Utils.IsEntity(newCellPosition).HP - Utils.GetRandomInt(0, 6);
            if (Utils.IsEntity(newCellPosition).HP <= 0)
            {
                Destroy(Utils.IsEntity(newCellPosition).Sprite);
                Utils.IsEntity(newCellPosition).Alive = false;
            }
        } 
    }

    public static void MoveToCell(Entity _entity, int x, int y)
    {
        _map = GameObject.Find("Map");

        //Vector3Int oldCellPosition = _entity.Position;
        Vector3Int newCellPosition = new Vector3Int(x, y, 0);

        if (Utils.IsTileEmpty(newCellPosition))
        {
            // if move is possible, update the entitys grid position and render position.
            _entity.Position = newCellPosition;
            _entity.Sprite.GetComponent<Transform>().position = _map.GetComponent<Grid>().GetCellCenterLocal(newCellPosition);
        }
        else if (!Utils.IsTileEmpty(newCellPosition) && Utils.IsEntity(newCellPosition) != null)
        {
            Debug.Log("Attack!");
        }
    }
}

