using System;
using System.Linq;
using System.Text;

public class MySetArray<T> : MySet<T>
{
    private T[] items;
    private int count;
    private const int DefaultCapacity = 10;

    public MySetArray(int capacity = DefaultCapacity)
    {
        items = new T[capacity];
        count = 0;
    }

    public override T[] Elements => items.Take(count).ToArray();

    public override void Add(T item)
    {
        if (Contains(item)) return;
        if (count == items.Length)
            Array.Resize(ref items, items.Length * 2);
        items[count++] = item;
    }

    public override void Remove(T item)
    {
        int index = Array.IndexOf(items, item, 0, count);
        if (index >= 0)
        {
            items[index] = items[--count];
            items[count] = default;
        }
    }

    public override void Clear() => count = 0;
    public override bool Contains(T item) => Array.IndexOf(items, item, 0, count) >= 0;
    public override int Cardinality() => count;
    public override bool IsEmpty() => count == 0;

    public override MySet<T> Union(MySet<T> other)
    {
        var result = new MySetArray<T>();
        foreach (var item in Elements) result.Add(item);
        foreach (var item in other.Elements) result.Add(item);
        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        var result = new MySetArray<T>();
        foreach (var item in Elements)
            if (other.Contains(item)) result.Add(item);
        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        var result = new MySetArray<T>();
        foreach (var item in Elements)
            if (!other.Contains(item)) result.Add(item);
        return result;
    }

    public override void Show()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        if (count == 0) return "{ }";
        var sb = new StringBuilder();
        sb.Append("{ ");
        for (int i = 0; i < count; i++)
        {
            sb.Append(items[i]);
            if (i < count - 1) sb.Append(", ");
        }
        sb.Append(" }");
        return sb.ToString();
    }
}
