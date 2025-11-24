using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PriorityQueue<T>
{
    class Node
    {
        public T item;
        public float priority;
        public Node(T item, float priority)
        {
            this.item = item;
            this.priority = priority;
        }
    }

    List<Node> heap = new List<Node>();

    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add(new Node(item, priority));
        HeapifyUP(heap.Count - 1);
    }

    public T Dequeue()
    {
        T result = heap[0].item;

        int last = heap.Count - 1;
        heap[0] = heap[last];
        heap.RemoveAt(last);

        HeapifyDown(0);

        return result;
    }

    public void HeapifyUP(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;

            if (heap[index].priority >= heap[parent].priority) break;

            Swap(index, parent);
            index = parent;
        }

    }

    public void HeapifyDown(int index)
    { 
        int last = heap.Count - 1;
        while (true) 
        { 
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int smallest = index;

            if(left <= last && heap[left].priority <= heap[smallest].priority) 
                smallest = left;
            if(right <= last && heap[right].priority <= heap[smallest].priority)
                smallest = right;
            if (smallest == index)
                break;
            Swap(index, smallest);
            index = smallest;

        }


    }

    public void Swap(int a, int b)
    {
        var temp = heap[a];
        heap[a] = heap[b];
        heap[b] = temp;
    }
}
