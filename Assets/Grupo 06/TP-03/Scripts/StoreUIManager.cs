using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public Store store;                
    public StoreManager storeManager;  
    public Transform contentPanel;    
    public GameObject itemButtonPrefab;

    void Start()
    {
        ShowStore();
    }

    public void ShowStore()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var keyValuePair in store.items)
        {

            Item item = keyValuePair.Value;

            GameObject newButton = Instantiate(itemButtonPrefab, contentPanel);

            newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            newButton.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = "$" + item.price.ToString();
            newButton.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            int itemID = item.id;
            newButton.GetComponent<Button>().onClick.AddListener(() => storeManager.BuyItem(itemID));
        }
    }
}
