using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLinkedList;
using static UnityEngine.Rendering.DebugUI;
using System;

public class MyList<T>
{
    private MyNode<T> root;
    private MyNode<T> tail;

    public int Count { get; private set; }

    public MyList()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    public void Add(T value)
    {
        MyNode<T> newNode = new MyNode<T>(value);
        
        if(root == null)
        {
            root = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
        Count++;
    }

    public void AddRange(MyList<T> values)
    {
        if(values == null || values.root == null)
        {
            return;
        }

        MyNode<T> current = values.root;

        while (current != null) 
        {
            this.Add(current.Data);
            current = current.Next;
        }
    }
    
    public void AddRange(T[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        foreach(T item in values)
        {
            Add(item);
        }
    }

    public bool Remove(T value)
    {
        if (value == null)
        {
            return false;
        }
        
        MyNode<T> current = root;

        while (current != null) 
        {
            if (current.Data.Equals(value))
            {
                if (current == root)
                {
                    root = current.Next;
                    if (root != null)
                    {
                        root.Prev = null;
                    }
                    else
                    {
                        tail = null;
                    }
                }
                else if (current == tail)
                {
                    tail = current.Prev;
                    tail.Next = null;
                }
                else
                {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                }

                Count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            Debug.Log("Index out of range.");
        }
        

    MyNode<T> current = root;
    for (int i = 0; i < index; i++)
        current = current.Next;

    if (current.Prev != null)
        current.Prev.Next = current.Next;
    else
        root = current.Next;

    if (current.Next != null)
        current.Next.Prev = current.Prev;
    else
        tail = current.Prev;

    Count--;
    }

    public T GetAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            Debug.Log("Index out of range");
        }

        MyNode<T> current = root;
        for (int i = 0; i < index; i++)
            current = current.Next;

        return current.Data;
    }

    public void Insert(int index, T value)
    {
        if (index < 0 || index >= Count)
        {
            Debug.Log("Index out of range.");
        }

        MyNode<T> newNode = new MyNode<T>(value);

        if (index == 0)
        {
            newNode.Next = root;
            if (root != null)
                root.Prev = newNode;
            root = newNode;

            if (tail == null)
                tail = newNode;

            Count++;
            return;
        }

        if (index == Count)
        {
            Add(value);
            return;
        }

        MyNode<T> current = root;
        for (int i = 0; i < index; i++)
            current = current.Next;

        newNode.Prev = current.Prev;
        newNode.Next = current;
        current.Prev.Next = newNode;
        current.Prev = newNode;

        Count++;
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }

    public void Clear()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    public override string ToString()
    {
       if(root == null)
        {
            return "List is Empty";
        }

        string result = "";
        MyNode<T> current = root;

        while (current != null) 
        {
            result += current.Data;
            if (current.Next != null)
                result += ",";
            current = current.Next;
        }
        return result;
    }

    public void BubbleSort(Comparison<T> comparison)
    {
        if (Count < 2) return; // si hay menos de 2 elementos, no hace falta ordenar

        bool swapped;
        do
        {
            swapped = false;
            MyNode<T> current = root;
            while (current != null && current.Next != null)
            {
                if (comparison(current.Data, current.Next.Data) > 0)
                {
                    // intercambiar valores
                    T temp = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = temp;
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped); // repetir hasta que no haya intercambios
    }

    public void SelectionSort(Comparison<T> comparison)
    {
        if (Count < 2) return; // si hay menos de 2 elementos, no hace falta ordenar

        MyNode<T> current = root;//empieza en el primer nodo

        while (current != null)
        {
            MyNode<T> minNode = current;//el nodo actual es el minimo
            MyNode<T> next = current.Next;//recorre los nodos siguientes

            //busca el nodo con el valor minimo
            while (next != null)
            {
                if (comparison(next.Data, minNode.Data) < 0)
                {
                    minNode = next;//ecuentra un nuevo minimo
                }
                next = next.Next;
            }

            if (minNode != current)//intercambia los datos si encontramos un nodo menor
            {
                T temp = current.Data;
                current.Data = minNode.Data;
                minNode.Data = temp;
            }

            current = current.Next;//avanza a siguiente nodo
        }
    }

    public void InsertionSort(Comparison<T> comparison)
    {
        if (Count < 2) return;// si hay menos de 2 elementos, no hace falta ordenar

        MyNode<T> current = root.Next;
        while (current != null)
        {
            T key = current.Data;
            MyNode<T> prev = current.Prev;

            // mover los nodos mayores que key hacia la derecha
            while (prev != null && comparison(prev.Data, key) > 0)
            {
                prev.Next.Data = prev.Data;
                prev = prev.Prev;
            }

            if (prev == null)
                root.Data = key; // insertar al inicio
            else
                prev.Next.Data = key; // insertar en su posicion correcta

            current = current.Next;
        }
    }


}
