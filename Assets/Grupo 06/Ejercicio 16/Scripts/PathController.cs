using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PathController : MonoBehaviour
{
    public static PathController Instance { get; private set; }

    [Header("UI Popup")]
    public GameObject ResultPanel;
    public TMP_Text ResultText;
    public TMP_Text currentPathText;

    [Header("Planets")]
    public string[] planets;

    [System.Serializable]
    public struct Edge { public string from; public string to; public int weight; }
    public Edge[] edges;

    private MyALGraph<string> g = new MyALGraph<string>();
    private readonly List<string> path = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (planets != null)
        {
            foreach (var p in planets)
                if (!string.IsNullOrWhiteSpace(p))
                    g.AddVertex(p.Trim());
        }

        if (edges != null)
        {
            foreach (var e in edges)
            {
                if (!string.IsNullOrWhiteSpace(e.from) && !string.IsNullOrWhiteSpace(e.to))
                    g.AddEdge(e.from.Trim(), e.to.Trim(), e.weight);
            }
        }

        if (ResultPanel != null) ResultPanel.SetActive(false);
        UpdatePathText();
    }

    public void AddPlanet(string name)
    {
        path.Add(name.Trim());
        UpdatePathText();
    }

    private void UpdatePathText()
    {
        if (currentPathText == null) return;
        currentPathText.text = path.Count == 0 ? " " : string.Join(" \u2192 ", path);
    }

    public void OnCalculate()
    {
        if (path.Count < 2)
        {
            Show($"Seleccioná al menos 2 planetas.");
            return;
        }

        int total = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            string a = path[i];
            string b = path[i + 1];
            int w = g.GetWeight(a, b);
            if (w == -1)
            {
                Show($"Camino inválido entre \"{a}\" y \"{b}\".");
                return;
            }
            total += w;
        }

        Show($"Camino válido — Costo total: {total}");
    }

    public void OnClear()
    {
        path.Clear();
        UpdatePathText();
        Hide();
    }

    private void Show(string msg)
    {
        if (ResultText != null) ResultText.text = msg;
        if (ResultPanel != null) ResultPanel.SetActive(true);
        else Debug.Log(msg);
    }

    private void Hide()
    {
        if (ResultPanel != null) ResultPanel.SetActive(false);
    }
}
