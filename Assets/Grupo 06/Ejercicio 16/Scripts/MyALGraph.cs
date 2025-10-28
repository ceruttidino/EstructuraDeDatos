using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyALGraph<T>
{
    private Dictionary<T, List<(T to, int weight)>> _adj = new Dictionary<T, List<(T, int)>>();

    public IEnumerable<T> Vertices => _adj.Keys;

    public void AddVertex(T vertex)
    {
        if (!_adj.ContainsKey(vertex))
            _adj[vertex] = new List<(T, int)>();
    }

    public void RemoveVertex(T vertex)
    {
        if (!_adj.ContainsKey(vertex)) return;

        _adj.Remove(vertex);
        foreach (var list in _adj.Values)
            list.RemoveAll(x => EqualityComparer<T>.Default.Equals(x.to, vertex));
    }

    public void AddEdge(T from, T to, int weight)
    {
        AddVertex(from);
        AddVertex(to);

        _adj[from].Add((to, weight));
        _adj[to].Add((from, weight));
    }

    public void RemoveEdge(T from, T to)
    {
        if (_adj.ContainsKey(from))
            _adj[from].RemoveAll(x => EqualityComparer<T>.Default.Equals(x.to, to));

        if (_adj.ContainsKey(to))
            _adj[to].RemoveAll(x => EqualityComparer<T>.Default.Equals(x.to, from));
    }

    public bool ContainsVertex(T vertex) => _adj.ContainsKey(vertex);

    public bool ContainsEdge(T from, T to)
    {
        return _adj.ContainsKey(from) &&
               _adj[from].Any(x => EqualityComparer<T>.Default.Equals(x.to, to));
    }

    public int GetWeight(T from, T to)
    {
        if (!_adj.TryGetValue(from, out var list)) return -1;

        var cmp = EqualityComparer<T>.Default;
        for (int i = 0; i < list.Count; i++)
        {
            var e = list[i];
            if (cmp.Equals(e.to, to))
                return e.weight;
        }
        return -1;
    }
}
