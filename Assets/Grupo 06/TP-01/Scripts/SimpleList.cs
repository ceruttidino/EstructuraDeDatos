using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public void BubbleSort(Comparison<T> comparison)
    {
        if (count < 2) return;

        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (comparison(list[j], list[j + 1]) > 0)
                {
                    
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    public void SelectionSort(Comparison<T> comparison)
    {
        for (int i = 0; i < count - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < count; j++)
            {
                if (comparison(list[j], list[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                T temp = list[i];
                list[i] = list[minIndex];
                list[minIndex] = temp;
            }
        }
    }

    public void InsertionSort(Comparison<T> comparison)
    {
        for (int i = 1; i < count; i++) 
        {
            T key = list[i];
            int j = i - 1;

            
            while (j >= 0 && comparison(list[j], key) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = key;
        }
    }


}
