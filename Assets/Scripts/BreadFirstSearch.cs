/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Example of non-grid graph
  public class Graph<Vector3Int>
{
    public Dictionary<Vector3Int, Vector3Int[]> edges = new Dictionary<Vector3Int, Vector3Int[]>();

    public Vector3Int[] Neighbors(Vector3Int id)
    {
        return edges[id];
    }
}

public class BreadFirstSearch
{
    private static int _width = GameMaster.width;
    private static int _height = GameMaster.height;
    private static Tilemap _tilemap = GameObject.Find("Map").transform.GetChild(0).GetComponent<Tilemap>();

    public static void Main()
    {
        WorldTile _tile;
        SquareGrid grid = new SquareGrid(GameMaster.width, GameMaster.height);

        //go through all the tiles, and check if they are passable, if not, put them into grid.walls[]
        foreach (Vector3Int pos in _tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (GameTiles.instance.tiles.TryGetValue(localPlace, out _tile))
            {
                if (!_tile.isWalkable)
                {
                    grid.walls.Add(new Vector3Int(_tile.LocalPlace.x, _tile.LocalPlace.y));
                }
            }
        }

    var path = Search(grid, new Vector3Int(3,3));
        int i = 0;
    foreach(Vector3Int loc in path)
        {
            i++;
            Debug.Log("Path " + i + ":  " + path);
        }
        
    }

    public static HashSet<Vector3Int> Search(SquareGrid grid, Vector3Int start)
    {
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(start);

        HashSet<Vector3Int> cameFrom = new HashSet<Vector3Int>
    {
        start
    };

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

           Debug.Log("Visiting " + current);
            foreach (Vector3Int next in grid.Neighbors(current))
            {
                Debug.Log("neighbor: " + current);

                if (!cameFrom.Contains(next))
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next);
                }
            }
        }
        return cameFrom;
    }


}


public class SquareGrid
    {
        public int _width;
        public int _height;
        public HashSet<Vector3Int> walls = new HashSet<Vector3Int>();

        public static readonly Vector3Int[] DIRS = new[]
        {
            new Vector3Int(1, 0),
            new Vector3Int(0, -1),
            new Vector3Int(-1, 0),
            new Vector3Int(0, 1)
    };

        public SquareGrid(int width, int height)
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


        public IEnumerable<Vector3Int> Neighbors(Vector3Int id)
        {
            foreach (var dir in DIRS)
            {
                Vector3Int next = new Vector3Int(id.x + dir.x, id.y + dir.y);
                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }

*/