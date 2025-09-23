using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Data { get; set; }
        public MyNode<T> Next { get; set; }
        public MyNode<T> Prev { get; set; }

        public MyNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
