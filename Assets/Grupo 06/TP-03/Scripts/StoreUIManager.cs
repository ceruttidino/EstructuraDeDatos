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
        ShowStore(store.SortItems("id"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            ShowStore(store.SortItems("name"));

        if (Input.GetKeyDown(KeyCode.P))
            ShowStore(store.SortItems("price"));

        if (Input.GetKeyDown(KeyCode.I))
            ShowStore(store.SortItems("id"));

        if (Input.GetKeyDown(KeyCode.R))
            ShowStore(store.SortItems("rarity"));

        if (Input.GetKeyDown(KeyCode.T))
            ShowStore(store.SortItems("type"));
    }

  
    public void ShowStore(List<Item> itemList)
    {
        
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        
        foreach (Item item in itemList)
        {
            GameObject newButton = Instantiate(itemButtonPrefab, contentPanel);

            
            newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            newButton.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = "$" + item.price.ToString();
            newButton.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            int itemID = item.id;
            newButton.GetComponent<Button>().onClick.AddListener(() => storeManager.BuyItem(itemID));
        }
    }
}
