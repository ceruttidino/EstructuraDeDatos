using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFindingAStar : MonoBehaviour
{
    GridManager gridManager;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>() ?? FindAnyObjectByType<GridManager>();
    }

    class Node
    {
        public int x, y;
        public float g, f;
        public Node parent;
        public Node(int x, int y) { this.x = x; this.y = y; g = Mathf.Infinity; f = Mathf.Infinity; }
        public override bool Equals(object obj)
        {
            if (!(obj is Node)) return false;
            Node n = (Node)obj;
            return n.x == x && n.y == y;
        }
        public override int GetHashCode()
        {
            return x * 10000 + y;
        }
    }

    List<Node> GetNeighbors(Node n)
    {
        List<Node> list = new List<Node>();
        int[,] offs = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        for (int i = 0; i < 4; i++)
        {
            int nx = n.x + offs[i, 0];
            int ny = n.y + offs[i, 1];
            Tile t = gridManager.GetTile(nx, ny);
            if (t != null && t.IsWalkable())
            {
                list.Add(new Node(nx, ny));
            }
        }
        return list;
    }

    float Heuristic(int x1, int y1, int x2, int y2)
    {
        return Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2);
    }

    public List<Tile> FindPath(Tile startTile, Tile endTile)
    {
        if (startTile == null || endTile == null) return null;
        if (!startTile.IsWalkable() || !endTile.IsWalkable()) return null;

        int sx = startTile.x;
        int sy = startTile.y;
        int ex = endTile.x;
        int ey = endTile.y;

        var open = new SortedSet<Node>(Comparer<Node>.Create((a, b) =>
        {
            int cmp = a.f.CompareTo(b.f);
            if (cmp == 0) cmp = a.g.CompareTo(b.g);
            if (cmp == 0) cmp = a.x.CompareTo(b.x);
            if (cmp == 0) cmp = a.y.CompareTo(b.y);
            return cmp;
        }));

        Dictionary<(int, int), Node> allNodes = new Dictionary<(int, int), Node>();

        Node start = new Node(sx, sy) { g = 0f, f = Heuristic(sx, sy, ex, ey) };
        open.Add(start);
        allNodes[(sx, sy)] = start;

        HashSet<(int, int)> closed = new HashSet<(int, int)>();

        while (open.Count > 0)
        {
            Node current = open.First();
            open.Remove(current);
            if (current.x == ex && current.y == ey)
            {
                List<Tile> path = new List<Tile>();
                Node p = current;
                while (p != null)
                {
                    path.Add(gridManager.GetTile(p.x, p.y));
                    p = p.parent;
                }
                path.Reverse();
                return path;
            }
            closed.Add((current.x, current.y));

            foreach (Node nbTemplate in GetNeighbors(current))
            {
                var key = (nbTemplate.x, nbTemplate.y);
                if (closed.Contains(key)) continue;

                if (!allNodes.TryGetValue(key, out Node neighbor))
                {
                    neighbor = new Node(nbTemplate.x, nbTemplate.y);
                    allNodes[key] = neighbor;
                }

                float tentativeG = current.g + 1f;

                if (tentativeG < neighbor.g)
                {
                    neighbor.parent = current;
                    neighbor.g = tentativeG;
                    neighbor.f = tentativeG + Heuristic(neighbor.x, neighbor.y, ex, ey);

                    if (open.Contains(neighbor))
                    {
                        open.Remove(neighbor);
                    }
                    open.Add(neighbor);
                }
            }


        }
        return null;
    }
}
