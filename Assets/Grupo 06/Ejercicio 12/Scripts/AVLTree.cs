using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AVLTree<T> : MyTree<T> where T : IComparable<T>
{
    private int Height(TreeNode<T> node)
    {
        return node == null ? 0 : node.Height;
    }

    private int GetBalance(TreeNode<T> node)
    {
        return node == null ? 0 : Height(node.Left) - Height(node.Right);
    }

    private TreeNode<T> RotateRight(TreeNode<T> y)
    {
        TreeNode<T> x = y.Left;
        TreeNode<T> T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

        return x;
    }

    private TreeNode<T> RotateLeft(TreeNode<T> x)
    {
        TreeNode<T> y = x.Right;
        TreeNode<T> T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
        y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

        return y;
    }

    public new void Insert(T data)
    {
        root = InsertAVL(root, data);
    }

    private TreeNode<T> InsertAVL(TreeNode<T> node, T data)
    {
        if (node == null)
            return new TreeNode<T>(data);

        int compare = data.CompareTo(node.Data);

        if (compare < 0)
            node.Left = InsertAVL(node.Left, data);
        else if (compare > 0)
            node.Right = InsertAVL(node.Right, data);
        else
            return node;

        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

        int balance = GetBalance(node);

        if (balance > 1 && data.CompareTo(node.Left.Data) < 0)
            return RotateRight(node);

        if (balance < -1 && data.CompareTo(node.Right.Data) > 0)
            return RotateLeft(node);

        if (balance > 1 && data.CompareTo(node.Left.Data) > 0)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (balance < -1 && data.CompareTo(node.Right.Data) < 0)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    public void InOrder()
    {
        InOrder(root);
    }

    public void PreOrder()
    {
        PreOrder(root);
    }

    public void PostOrder()
    {
        PostOrder(root);
    }

    public void LevelOrder()
    {
        if (root == null) return;

        Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode<T> current = queue.Dequeue();
            Debug.Log(current.Data);

            if (current.Left != null)
                queue.Enqueue(current.Left);

            if (current.Right != null)
                queue.Enqueue(current.Right);
        }
    }
}
