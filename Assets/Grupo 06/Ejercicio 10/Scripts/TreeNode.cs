using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TreeNode<T>
{
    public T Data;
    public TreeNode<T> Left;
    public TreeNode<T> Right;

    public TreeNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
