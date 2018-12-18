using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public int x { get; set; }
    public int y { get; set; }
    public Vector3Int position { get; set; }
    public string name { get; set; }
    public GameObject sprite { get; set; }
    public bool needsUserInput = false;

    public void DoAction()
    {
        Debug.Log(this.name);
    }

    public void MoveTo (int x, int y)
        {
        EntityMover.MoveToPosition(x, y, this);
        }

}
