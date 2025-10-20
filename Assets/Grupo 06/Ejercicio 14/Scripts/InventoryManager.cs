using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text leftInventoryText;
    public TMP_Text rightInventoryText;
    public TMP_Text resultText;
    public TMP_Text countsText;
    public Button btnCommon;
    public Button btnNone;
    public Button btnUnion;
    public Button btnCounts;

    public int totalDistinctItems = 40;
    public int slotsPerInventory = 20;
    [Range(0f, 1f)] public float slotOccupiedChance = 0.7f;

    private System.Random rnd = new();
    private MySetArray<Item2> leftInventory;
    private MySetArray<Item2> rightInventory;
    private List<Item2> allItems;

    void Start()
    {
        GenerateAllItems();
        CreateInventories();

        if (btnCommon != null) btnCommon.onClick.AddListener(ShowCommon);
        if (btnNone != null) btnNone.onClick.AddListener(ShowNone);
        if (btnUnion != null) btnUnion.onClick.AddListener(ShowUnion);
        if (btnCounts != null) btnCounts.onClick.AddListener(ShowCounts);

        ShowAllInventories();
    }

    void GenerateAllItems()
    {
        allItems = new List<Item2>(totalDistinctItems);
        for (int i = 1; i <= totalDistinctItems; i++)
        {
            float price = (float)Math.Round(rnd.NextDouble() * 499 + 1, 2);
            allItems.Add(new Item2($"Item {i}", price));
        }
    }

    void CreateInventories()
    {
        leftInventory = new MySetArray<Item2>();
        rightInventory = new MySetArray<Item2>();

        FillInventory(leftInventory);
        FillInventory(rightInventory);
    }

    void FillInventory(MySetArray<Item2> inventory)
    {
        for (int s = 0; s < slotsPerInventory; s++)
        {
            if (rnd.NextDouble() < slotOccupiedChance)
            {
                var item = allItems[rnd.Next(allItems.Count)];
                inventory.Add(item);
            }
        }
    }

    void ShowAllInventories()
    {
        leftInventoryText.text = FormatInventory(leftInventory, "Izquierda");
        rightInventoryText.text = FormatInventory(rightInventory, "Derecha");
        resultText.text = "Elige un botón para mostrar resultados.";
        countsText.text = $"Izquierda: {leftInventory.Cardinality()}    Derecha: {rightInventory.Cardinality()}";
    }

    string FormatInventory(MySetArray<Item2> inv, string title)
    {
        var elems = inv.Elements;
        var lines = elems.Length == 0 ? new string[] { "(vacío)" } : elems.Select(i => i.ToString()).ToArray();
        return $"Inventario {title} ({inv.Cardinality()}):\n" + string.Join("\n", lines);
    }

    void ShowCommon()
    {
        var intersectionSet = leftInventory.Intersect(rightInventory);
        var arr = intersectionSet.Elements;
        resultText.text = FormatResult(arr, "Ítems en común");
    }

    void ShowUnion()
    {
        var unionSet = leftInventory.Union(rightInventory);
        var arr = unionSet.Elements;
        resultText.text = FormatResult(arr, "Unión (todos los ítems entre ambos)");
    }

    void ShowNone()
    {
        var unionSet = leftInventory.Union(rightInventory);
        var unionElems = new HashSet<Item2>(unionSet.Elements); 
        var noneList = allItems.Where(i => !unionElems.Contains(i)).ToArray();
        resultText.text = FormatResult(noneList, "Ítems que no tiene ninguno");
    }

    void ShowCounts()
    {
        var inter = leftInventory.Intersect(rightInventory);
        var uni = leftInventory.Union(rightInventory);

        countsText.text = $"Izquierda: {leftInventory.Cardinality()}    Derecha: {rightInventory.Cardinality()}\n" +
                          $"En común: {inter.Cardinality()}    Total distintos: {uni.Cardinality()}";
        resultText.text = "Conteos actualizados.";
    }

    string FormatResult(Item2[] items, string title)
    {
        if (items == null || items.Length == 0)
            return $"{title}:\n(none)";
        return $"{title} ({items.Length}):\n" + string.Join("\n", items.Select(i => i.ToString()));
    }
}
