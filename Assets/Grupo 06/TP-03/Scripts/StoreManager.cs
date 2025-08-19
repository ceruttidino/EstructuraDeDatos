using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class StoreManager : MonoBehaviour
{
   private Store store;
   private PlayerInventory playerInventory;

    void Awake()
    {
        if (store == null) store = FindObjectOfType<Store>();
        if (playerInventory == null) playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void BuyItem(int itemID)
    {
        if (store.items.ContainsKey(itemID))
        {
            Item item = store.items[itemID];

            if (playerInventory.money >= item.price)
            {
                playerInventory.money -= item.price;

                if (playerInventory.inventory.ContainsKey(itemID))
                {
                    playerInventory.inventory[itemID] = (item, playerInventory.inventory[itemID].quantity + 1);
                }
                else
                {
                    playerInventory.inventory.Add(itemID, (item, 1));
                }

                Debug.Log("You bought: " + item.name);

                store.items.Remove(itemID);

                FindObjectOfType<StoreUI>().ShowStore();
                FindObjectOfType<InventoryUI>().ShowInventory();
                FindObjectOfType<MoneyUI>().UpdateMoneyUI();
            }
            else 
            {
                Debug.Log("You don´t have enough money");
            }
        }
    }

    public void SellItem(int itemID)
    {
        if (playerInventory.inventory.ContainsKey(itemID))
        {
            Item item = playerInventory.inventory[itemID].item;

            playerInventory.money += item.price;

            playerInventory.inventory[itemID] = (item, playerInventory.inventory[itemID].quantity - 1);

            if (playerInventory.inventory[itemID].quantity <= 0)
            {
                playerInventory.inventory.Remove(itemID);
            }

            if (!store.items.ContainsKey(itemID))
            {
                store.items.Add(itemID, item);
            }

            FindObjectOfType<InventoryUI>().ShowInventory();
            FindObjectOfType<StoreUI>().ShowStore();
            FindObjectOfType<MoneyUI>().UpdateMoneyUI();

            Debug.Log("You sold: " + item.name);
        }
        else
        {
            Debug.Log("You don’t have that item to sell");
        }
    }
}
