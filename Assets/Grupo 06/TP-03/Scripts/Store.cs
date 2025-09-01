using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    public Dictionary<int, Item> items = new Dictionary<int, Item>();

    void Awake()
    {
        Item sword = new Item { id = 01, name = "Sword", price = 100, rarity = "Common", type = "Weapon" };
        Item axe = new Item { id = 02, name = "Axe", price = 150, rarity = "Common", type = "Weapon" };
        Item shield = new Item { id = 03, name = "Shield", price = 100, rarity = "Common", type = "Shield" };
        Item healthPotion = new Item { id = 04, name = "Health Potion", price = 200, rarity = "Rare", type = "Consumable" };
        Item armour = new Item { id = 05, name = "Armour", price = 300, rarity = "Exotic", type = "Armour" };

        items.Add(sword.id, sword);
        items.Add(axe.id, axe);
        items.Add(shield.id, shield);
        items.Add(healthPotion.id, healthPotion);
        items.Add(armour.id, armour);
    }

    public List<Item> SortItems(string criteria)// Metodo que devuelve una lista de items ordenada segun un criterio
    {
        List<Item> itemList = new List<Item>(items.Values);// Convertir los items del diccionario a una lista

        Comparison<Item> comparison;

        switch (criteria.ToLower())// Definir la función de comparacion según el criterio
        {
            case "id":
                comparison = (a, b) => a.id.CompareTo(b.id);// Ordenar por ID
                break;
            case "nombre":
                comparison = (a, b) => a.name.CompareTo(b.name);// Ordenar por nombre
                break;
            case "precio":
                comparison = (a, b) => a.price.CompareTo(b.price);// Ordenar por precio
                break;
            case "tipo":
                comparison = (a, b) => a.type.CompareTo(b.type);// Ordenar por tipo
                break;
            case "rareza":
                comparison = (a, b) => GetRarityValue(a.rarity).CompareTo(GetRarityValue(b.rarity));// Ordenar por rareza
                break;
            default:
                Debug.LogWarning("no se ordeno");
                return itemList;
        }

        SelectionSort(itemList, comparison);// Aplicar SelectionSort 

        return itemList;
    }

    // SelectionSort
    private void SelectionSort(List<Item> list, Comparison<Item> comparison)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                // Si el item en j es menor que el item en minIndex, actualizar minIndex
                if (comparison(list[j], list[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            // Intercambiar los items si encuentra uno menor
            if (minIndex != i)
            {
                Item temp = list[i];
                list[i] = list[minIndex];
                list[minIndex] = temp;
            }
        }
    }

    //Rarezas para que no sea alfabetico
    private int GetRarityValue(string rarity)
    {
        switch (rarity.ToLower())
        {
            case "common": return 1;
            case "rare": return 2;
            case "exotic": return 3;
            default: return 99;// Valor alto para rarezas desconocidas
        }
    }
}