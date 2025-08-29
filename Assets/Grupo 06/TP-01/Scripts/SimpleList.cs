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
        if (count < 2) return;// si hay 0 o 1 elementos no hay que ordenar

        for (int i = 0; i < count - 1; i++)//recore los elementos de la lista
        {
            for (int j = 0; j < count - i - 1; j++)//compara elementos
            {
                if (comparison(list[j], list[j + 1]) > 0)//si el elemento es mayor que el siguiente segun la comparacion
                {
                    //intercambia los elementos
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    public void SelectionSort(Comparison<T> comparison)
    {
        for (int i = 0; i < count - 1; i++)//recorre todos los elementos menos el ultimo
        {
            int minIndex = i;//el elemento actual es el mas chico 

            for (int j = i + 1; j < count; j++)//busca el elemento mas chico de la lista
            {
                if (comparison(list[j], list[minIndex]) < 0)//si encuentra un elemento menor actualiza el min index
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)//cambia el minimo
            {
                T temp = list[i];
                list[i] = list[minIndex];
                list[minIndex] = temp;
            }
        }
    }

    public void InsertionSort(Comparison<T> comparison)
    {
        for (int i = 1; i < count; i++) // empieza desde el segundo elemento
        {
            T key = list[i]; // elemento que tiene que insertar
            int j = i - 1;

            // mover los elementos mayores que key una posicion a la derecha
            while (j >= 0 && comparison(list[j], key) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }

            list[j + 1] = key; // insertar key en su posicion correcta
        }
    }


}
