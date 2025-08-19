using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int money = 1000;

    public Dictionary<int, (Item item, int quantity)> inventory = new Dictionary<int, (Item, int)>();
}
