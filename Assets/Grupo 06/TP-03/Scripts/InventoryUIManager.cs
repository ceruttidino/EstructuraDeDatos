using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Transform contentPanel;
    public GameObject itemButtonPrefab;

    public void ShowInventory()
    {
        
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        
        foreach (var keyValuePair in playerInventory.inventory)
        {
            Item item = keyValuePair.Value.item;
            int quantity = keyValuePair.Value.quantity;

            GameObject newItem = Instantiate(itemButtonPrefab, contentPanel);

            newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            newItem.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>().text = "x" + quantity;
            newItem.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            
            int itemID = item.id;
            newItem.GetComponent<Button>().onClick.AddListener(() => {
                FindObjectOfType<StoreManager>().SellItem(itemID);
                ShowInventory();
            });
        }
    }
}