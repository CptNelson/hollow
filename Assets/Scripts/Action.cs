using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    bool IsCompleted { get; set; }
    void Execute();
}

public class Walk : IAction
{
    private readonly Entity _entity;
    private readonly int _x, _y;

    public bool IsCompleted { get; set; }

    public Walk(Entity entity, int x, int y)
    {
        _entity = entity;
        _x = x;
        _y = y;
        IsCompleted = false;
    }

    public void Execute()
    {
        EntityMover.MoveToPosition(_entity, _x, _y);
        IsCompleted = true;
    }
}
public class SayName : IAction
{
    private readonly Entity _entity;

    public bool IsCompleted { get; set; }
    public SayName(Entity entity)
    {
        _entity = entity;

        IsCompleted = false;
    }

    public void Execute()
    {
        //Debug.Log(_entity.Name);
        Geometry.DrawOctant();
        IsCompleted = true;
    }
}




