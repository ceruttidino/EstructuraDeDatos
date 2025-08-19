using System.Collections;
using System.Collections.Generic;
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

}