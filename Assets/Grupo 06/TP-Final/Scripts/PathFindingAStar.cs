using System.Collections.Generic;
using UnityEngine;

public class PathFindingAStar : MonoBehaviour
{
    GridManager gridManager;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>() ?? FindFirstObjectByType<GridManager>();
    }

    class Node
    {
        public int x, y;
        public float g, f;
        public Node parent;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            g = float.PositiveInfinity;
            f = float.PositiveInfinity;
        }
    }

    IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
    {
        int[,] offs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

        for (int i = 0; i < 4; i++)
        {
            int nx = x + offs[i, 0];
            int ny = y + offs[i, 1];
            Tile t = gridManager.GetTile(nx, ny);

            if (t != null && t.IsWalkable())
                yield return (nx, ny);
        }
    }

    float Heuristic(int x1, int y1, int x2, int y2)
    {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }

    public List<Tile> FindPath(Tile startTile, Tile endTile)
    {
        if (startTile == null || endTile == null) return null;
        if (!startTile.IsWalkable() || !endTile.IsWalkable()) return null;

        int sx = startTile.x, sy = startTile.y;
        int ex = endTile.x, ey = endTile.y;

        // Caso trivial
        if (sx == ex && sy == ey)
            return new List<Tile>() { startTile };

        int W = gridManager.width;
        int H = gridManager.height;

        // Reutilización de nodos ? sin garbage
        Node[,] nodes = new Node[W, H];
        for (int i = 0; i < W; i++)
            for (int j = 0; j < H; j++)
                nodes[i, j] = new Node(i, j);

        Node start = nodes[sx, sy];
        start.g = 0;
        start.f = Heuristic(sx, sy, ex, ey);

        var open = new PriorityQueue<Node>();
        open.Enqueue(start, start.f);

        bool[,] closed = new bool[W, H];

        while (open.Count > 0)
        {
            Node current = open.Dequeue();

            if (current.x == ex && current.y == ey)
                return ReconstructPath(nodes[ex, ey], gridManager);

            closed[current.x, current.y] = true;

            foreach (var nb in GetNeighbors(current.x, current.y))
            {
                int nx = nb.x, ny = nb.y;
                Node neighbor = nodes[nx, ny];

                if (closed[nx, ny])
                    continue;

                float tentativeG = current.g + 1;

                if (tentativeG < neighbor.g)
                {
                    neighbor.parent = current;
                    neighbor.g = tentativeG;
                    neighbor.f = tentativeG + Heuristic(nx, ny, ex, ey);

                    open.Enqueue(neighbor, neighbor.f);
                }
            }
        }

        // No existe camino
        return null;
    }


    List<Tile> ReconstructPath(Node endNode, GridManager gm)
    {
        List<Tile> path = new List<Tile>();
        Node n = endNode;

        while (n != null)
        {
            path.Add(gm.GetTile(n.x, n.y));
            n = n.parent;
        }

        path.Reverse();
        return path;
    }
}
