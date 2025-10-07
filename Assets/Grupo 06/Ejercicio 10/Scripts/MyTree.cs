using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MyTree<T>
{
    public TreeNode<T> root { get; private set; }

    public void Insert(T data)
    {
        root = InsertRecursive(root, data);
    }

    private TreeNode<T> InsertRecursive(TreeNode<T> node, T data)
    {
        if (node == null)
        {
            return new TreeNode<T>(data);
        }

        int compare = Comparer<T>.Default.Compare(data, node.Data);

        if (compare < 0)
        {
            node.Left = InsertRecursive(node.Left, data);
        }
            
        else if (compare > 0)
        {
            node.Right = InsertRecursive(node.Right, data);
        }

        return node;
    }
    public int GetHeight()
    {
        return GetHeightRecursive(root);
    }

    private int GetHeightRecursive(TreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        int leftHeight = GetHeightRecursive(node.Left);
        int rightHeight = GetHeightRecursive(node.Right);

        return Mathf.Max(leftHeight, rightHeight) + 1;
    }

    public int GetBalanceFactor(TreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        int left = GetHeightRecursive(node.Left);
        int right = GetHeightRecursive(node.Right);

        return left - right;
    }

    public void InOrder(TreeNode<T> node)
    {
        if (node == null) return;

        InOrder(node.Left);
        Debug.Log(node.Data);
        InOrder(node.Right);
    }

    public void PreOrder(TreeNode<T> node)
    {
        if (node == null) return;

        Debug.Log(node.Data);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    public void PostOrder(TreeNode<T> node)
    {
        if (node == null) return;

        PostOrder(node.Left);
        PostOrder(node.Right);
        Debug.Log(node.Data);
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
