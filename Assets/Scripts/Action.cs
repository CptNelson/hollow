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


public class Patrol : IAction
{
    private readonly Entity _entity;
    private readonly Vector3Int _goal;
    private List<Vector3Int>_path;

    public bool IsCompleted { get; set; }

    public Patrol(Entity entity, Vector3Int goal)
    {
        _entity = entity;
        _goal = goal;
        List<Vector3Int> path = new List<Vector3Int>();
        _path = GetAStarPath.ReconstructPath(_entity.Position, _goal);

        if (_path.Count <=1)
        {
            Debug.Log("done!");
            _entity.Goal = Utils.GetRandomEmptyPosition();
            _path.Clear();
            _path = GetAStarPath.ReconstructPath(_entity.Position, _goal);
            Debug.Log("new: " + _path[0]);
        }

        IsCompleted = false;
    }

    public void Execute()
    {

        EntityMover.MoveToCell(_entity, _path[0].x, _path[0].y);
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
        Debug.Log(_entity.Position);

        IsCompleted = true;
    }
}
public class GoTo : IAction
{
    private readonly Vector3Int _start, _goal;
    private Entity _entity;
    public bool IsCompleted { get; set; }
    public GoTo(Entity entity, Vector3Int start, Vector3Int goal)
    {
        _start = start;
        _goal = goal;
        _entity = entity;

        IsCompleted = false;
    }

    public void Execute()
    {
       List<Vector3Int> path = new List<Vector3Int>();
       path = GetAStarPath.ReconstructPath(_start, _goal);

        foreach (Vector3Int loc in path)
        {
            Debug.Log("A : " + loc);
        }

        EntityMover.MoveToCell(_entity, path[0].x, path[0].y);

        IsCompleted = true;
        
    }
}



