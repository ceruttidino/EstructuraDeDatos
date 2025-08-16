using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Data { get; set; } //data type
        public MyNode<T> Next { get; set; } //next node
        public MyNode<T> Prev { get; set; } //previous node

        public MyNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
}
