
using System;

public abstract class MySet<T>
{
   public abstract T[] Elements { get; }

    public abstract void Add(T items);
    public abstract void Remove(T items);
    public abstract void Clear();
    public abstract bool Contains(T item);
    public abstract int Cardinality();
    public abstract bool IsEmpty();

    public abstract MySet<T> Union(MySet<T> other);
    public abstract MySet<T> Intersect(MySet<T> other);
    public abstract MySet<T> Difference(MySet<T> other);

    public abstract void Show();

    public override string ToString()
    {
        return string.Join(", ", Elements);
    }
   
}
