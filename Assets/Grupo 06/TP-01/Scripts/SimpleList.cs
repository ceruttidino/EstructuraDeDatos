using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleList<T> : ISimpleList<T>
{

    int count = 0;
    const int initialSize = 10;
    T[] list = new T[initialSize];

    public T this[int index] { get { return list[index]; } set { list[index] = value; } }

    public int Count => count;

    public void Add(T item)
    {
        list[count++] = item;
    }

    public void AddRange(T[] collection)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            Add(collection[i]);
        }
    }

    public void Clear()
    {
        list = new T[initialSize];
        count = 0;
    }

    public bool Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (list[i] != null && list[i].Equals(item))
            {
                for (int j = i; j < count - 1; j++)
                {
                    list[j] = list[j + 1];
                }

                list[count - 1] = default;
                count--;

                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        if (count == 0)
            return "(vacío)";

        return string.Join(", ", list.Take(count));
    }

}
