using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQueue<T>
{
    private MyList<T> items = new MyList<T>();

    public int Count { get { return items.Count; } }

    public void Enqueue(T item)
    {
        items.Add(item);
    }

    public T Dequeue()
    {
        if (Count == 0)
        {
            Debug.Log("La cola esta vacia");
        }

        T value = items.GetAt(0);
        items.RemoveAt(0);
        return value;
    }

    public T Peek()
    {
        if (Count == 0)
        {
            Debug.Log("La cola esta vacia");
        }

        return items.GetAt(0);
    }

    public void Clear()
    {
        items.Clear();
    }

    public T[] ToArray()
    {
        T[] array = new T[Count];
        for (int i = 0; i < Count; i++)
        {
            array[i] = items.GetAt(i);
        }
        return array;
    }

    public override string ToString()
    {
        return items.ToString();
    }

    public bool TryDequeue(out T item)
    {
        if(Count == 0)
        {
            item = default(T);
            return false;
        }

        item = Dequeue();
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (Count == 0)
        {
            item = default(T); 
            return false;
        }

        item = Peek();
        return true;
    }
}
