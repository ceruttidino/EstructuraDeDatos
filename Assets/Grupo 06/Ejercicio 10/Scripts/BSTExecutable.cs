using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BSTExecutable : MonoBehaviour
{
    public GameObject treeNodePrefab;

    private MyTree<int> tree;

    public float spaceX = 2.5f;

    public float spaceY = -2f;

    public Vector2 startPosition = new Vector2(0, 6);

    public TextMeshProUGUI treeText;

    public void Start()
    {
        int[] myCustomArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

        tree = new MyTree<int>();

        foreach (int i in myCustomArray) 
        {
            tree.Insert(i);
        }

        treeText.text =
        "Altura del árbol: " + tree.GetHeight() + "\n" +
        "InOrder: " + GetTraversal(tree.root, "in") + "\n" +
        "PreOrder: " + GetTraversal(tree.root, "pre") + "\n" +
        "PostOrder: " + GetTraversal(tree.root, "post") + "\n" +
        "LevelOrder: " + GetLevelOrder(tree);

        CreateText($"Altura del árbol: {tree.GetHeight()}", new Vector2(-4, 4));

        DrawTree(tree.root, startPosition, 0);

    }

    void CreateText(string content, Vector2 pos)
    {
        GameObject go = new GameObject("UIText");
        var text = go.AddComponent<TextMeshProUGUI>();
        text.text = content;
        text.fontSize = 4;
        go.transform.position = pos;
    }

    void DrawTree(TreeNode<int> node, Vector2 position, int depth)
    {
        if (node == null) return;

        
        GameObject newNode = Instantiate(treeNodePrefab, position, Quaternion.identity);
        newNode.GetComponentInChildren<TextMeshProUGUI>().text = node.Data.ToString();

        if (node.Left != null)
            DrawTree(node.Left, position + new Vector2(-spaceX / (depth + 1), spaceY), depth + 1);

        if (node.Right != null)
            DrawTree(node.Right, position + new Vector2(spaceX / (depth + 1), spaceY), depth + 1);
    }

    string GetTraversal(TreeNode<int> node, string type)
    {
        if (node == null)
        {
            return "";
        }

        if (type == "in")
        {
            return GetTraversal(node.Left, "in") + node.Data + ", " + GetTraversal(node.Right, "in");
        }
            

        if (type == "pre")
        {
            return node.Data + ", " + GetTraversal(node.Left, "pre") + GetTraversal(node.Right, "pre");
        }
            

        if (type == "post")
        {
            return GetTraversal(node.Left, "post") + GetTraversal(node.Right, "post") + node.Data + ", ";
        }

        return "";
    }

    string GetLevelOrder(MyTree<int> tree)
    {
        if (tree.root == null)
        {
            return "";
        }

        Queue<TreeNode<int>> q = new Queue<TreeNode<int>>();
        q.Enqueue(tree.root);

        string result = "";

        while (q.Count > 0)
        {
            var node = q.Dequeue();
            result += node.Data + ", ";

            if (node.Left != null) q.Enqueue(node.Left);
            if (node.Right != null) q.Enqueue(node.Right);
        }

        return result;
    }
}
