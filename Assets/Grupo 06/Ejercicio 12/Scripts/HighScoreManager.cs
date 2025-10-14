using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;

    public TMP_InputField scoreInput;

    private AVLTree<int> scoreTree = new AVLTree<int>();

    void Start()
    {
        
        for (int i = 0; i < 100; i++)
            scoreTree.Insert(Random.Range(0, 500));

        UpdateUI();
    }

    public void AddScore(int score)
    {
        scoreTree.Insert(score);
        UpdateUI();
    }

    void UpdateUI()
    {
        List<int> scores = new List<int>();
        GetScoresDescending(scoreTree.root, scores);
        scoreText.text = string.Join("\n", scores);
    }

    void GetScoresDescending(TreeNode<int> node, List<int> list)
    {
        if (node == null) return;
        GetScoresDescending(node.Right, list);
        list.Add(node.Data);
        GetScoresDescending(node.Left, list);
    }

    public void PrintPreOrder()
    {
        Debug.Log("PreOrder:");
        scoreTree.PreOrder();

    }

    public void PrintInOrder()
    {
        Debug.Log("InOrder:");
        scoreTree.InOrder();
    }

    public void PrintPostOrder()
    {
        Debug.Log("PostOrder:");
        scoreTree.PostOrder();
    }

    public void PrintLevelOrder()
    {
        Debug.Log("LevelOrder:");
        scoreTree.LevelOrder();
    }

    public void AddScoreFromField()
    {
        if (scoreInput == null) return;

        if (int.TryParse(scoreInput.text, out int value))
        {
            AddScore(value);
            scoreInput.text = ""; 
        }
        else
        {
            Debug.LogWarning("Ingresá un número válido.");
        }
    }
}
