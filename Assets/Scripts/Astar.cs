using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//DO TO: Change Location to Vector3Int 

// A* needs only a WeightedGraph and a location type L, and does *not*
// have to be a grid. However, in the example code I am using a grid.
public interface WeightedGraph<L>
{
    double Cost(Vector3Int a, Vector3Int b);
    IEnumerable<Vector3Int> Neighbors(Vector3Int id);
}



public class WeightedSquareGrid : WeightedGraph<Vector3Int>
{
    // Implementation notes: I made the fields public for convenience,
    // but in a real project you'll probably want to follow standard
    // style and make them private.

    public static readonly Vector3Int[] DIRS = new[]
        {
            new Vector3Int(1, 0,0),
            new Vector3Int(0, -1,0),
            new Vector3Int(-1, 0,0),
            new Vector3Int(0, 1,0)
        };

    public int _width, _height;
    public HashSet<Vector3Int> walls = new HashSet<Vector3Int>();
    public HashSet<Vector3Int> bushes = new HashSet<Vector3Int>();

    public WeightedSquareGrid(int width, int height)
    {
        this._width = width;
        this._height = height;
    }

    public bool InBounds(Vector3Int id)
    {
        return 0 <= id.x && id.x < _width
            && 0 <= id.y && id.y < _height;
    }

    public bool Passable(Vector3Int id)
    {
        return !walls.Contains(id);
    }

    public double Cost(Vector3Int a, Vector3Int b)
    {
        return bushes.Contains(b) ? 5 : 1;
    }

    public IEnumerable<Vector3Int> Neighbors(Vector3Int id)
    {
        foreach (var dir in DIRS)
        {
            Vector3Int next = new Vector3Int(id.x + dir.x, id.y + dir.y, 0);
            if (InBounds(next) && Passable(next))
            {
                yield return next;
            }
        }
    }
}


public class PriorityQueue<T>
{
    // I'm using an unsorted array for this example, but ideally this
    // would be a binary heap. There's an open issue for adding a binary
    // heap to the standard C# library: https://github.com/dotnet/corefx/issues/574
    //
    // Until then, find a binary heap class:
    // * https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp
    // * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
    // * http://xfleury.github.io/graphsearch.html
    // * http://stackoverflow.com/questions/102398/priority-queue-in-net

    private List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, double priority)
    {
        elements.Add(Tuple.Create(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Item2 < elements[bestIndex].Item2)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Item1;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}


/* NOTE about types: in the main article, in the Python code I just
 * use numbers for costs, heuristics, and priorities. In the C++ code
 * I use a typedef for this, because you might want int or double or
 * another type. In this C# code I use double for costs, heuristics,
 * and priorities. You can use an int if you know your values are
 * always integers, and you can use a smaller size number if you know
 * the values are always small. */

public class AStarSearch
{
    public Dictionary<Vector3Int, Vector3Int> cameFrom
        = new Dictionary<Vector3Int, Vector3Int>();
    public Dictionary<Vector3Int, double> costSoFar
        = new Dictionary<Vector3Int, double>();

    // Note: a generic version of A* would abstract over Location and
    // also Heuristic
    static public double Heuristic(Vector3Int a, Vector3Int b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    public AStarSearch(WeightedGraph<Vector3Int> graph, Vector3Int start, Vector3Int goal)
    {
        var frontier = new PriorityQueue<Vector3Int>();
        frontier.Enqueue(start, 0);

        cameFrom[start] = start;
        costSoFar[start] = 0;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            foreach (var next in graph.Neighbors(current))
            {
                double newCost = costSoFar[current]
                    + graph.Cost(current, next);
                if (!costSoFar.ContainsKey(next)
                    || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    double priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }
    }
}

public class GetAStarPath
{
    private static Tilemap _tilemap;

    private static AStarSearch astar;

    public static List<Vector3Int> ReconstructPath(Vector3Int start, Vector3Int goal)
    {
        _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();

        var grid = new WeightedSquareGrid(GameMaster.width, GameMaster.height);

        foreach (Vector3Int pos in _tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (GameTiles.instance.tiles.TryGetValue(localPlace, out WorldTile _tile))
            {
                if (!_tile.isWalkable)
                {
                    grid.walls.Add(new Vector3Int(_tile.LocalPlace.x, _tile.LocalPlace.y,0));
                }
            }
        }


        astar = new AStarSearch(grid, new Vector3Int(start.x, start.y,0),
                                   new Vector3Int(goal.x, goal.y,0));


        var _current = goal;
        List<Vector3Int> path = new List<Vector3Int>();
        
        while(_current != start)
        {
            path.Add(_current);
            _current = astar.cameFrom[_current];
        }
        path.Reverse();
        return path;
    }


}