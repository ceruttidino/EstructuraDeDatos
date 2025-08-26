using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStack<T>
{
    private MyList<T> items = new MyList<T>();

    public int Count { get; private set; }

    public void Push(T item)
    {
        items.Add(item);
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
        {
            Debug.Log("Stack Vacio");
        }
        T value = items.GetAt(Count - 1);
        items.RemoveAt(Count - 1);
        Count--;
        return value;
    }

    public T Peek()
    {
        if (Count == 0)
        {
            Debug.Log("Stack Vacio");
        }
        return items.GetAt(Count - 1);
    }

    public void Clear()
    {
        items.Clear();
        Count = 0;
    }

    public T[] ToArray()
    {
        T[] array = new T[Count];

        for (int i = 0; i < Count; i++)
        {
            array[i] = items.GetAt(Count - 1 - i);
        }
        return array;
    }

    public override string ToString()
    {
        return "[" + string.Join(", ", ToArray()) + "]";
    }

    public bool TryPop(out T item)
    {
        if(Count == 0)
        {
            item = default;
            return false;
        }

        item = Pop();
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (Count == 0)
        {
            item = default;
            return false;
        }

        item = Peek();
        return true;
    }

}
