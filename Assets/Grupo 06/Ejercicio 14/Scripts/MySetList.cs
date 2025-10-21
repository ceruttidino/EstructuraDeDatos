using System;
using System.Collections.Generic;

public class MySetList<T> : MySet<T>
{
    private List<T> items = new();

    public override T[] Elements => items.ToArray();

    public override void Add(T item)
    {
        if (!items.Contains(item))
            items.Add(item);
    }

    public override void Remove(T item) => items.Remove(item);
    public override void Clear() => items.Clear();
    public override bool Contains(T item) => items.Contains(item);
    public override int Cardinality() => items.Count;
    public override bool IsEmpty() => items.Count == 0;

    public override MySet<T> Union(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in Elements) result.Add(item);
        foreach (var item in other.Elements) result.Add(item);
        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in Elements)
            if (other.Contains(item)) result.Add(item);
        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        var result = new MySetList<T>();
        foreach (var item in Elements)
            if (!other.Contains(item)) result.Add(item);
        return result;
    }

    public override void Show()
    {
        Console.WriteLine(ToString());
    }
}
