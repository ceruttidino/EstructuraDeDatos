using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;

public class StoreUI : MonoBehaviour
{
    public Store store;
    public StoreManager storeManager;
    public Transform contentPanel;
    public GameObject itemButtonPrefab;
    public TMP_Dropdown dropdown;

    void Start()
    {
        ShowStore(store.SortItems("id"));
        dropdown.onValueChanged.AddListener(ChangeSort);
    }

    private void Update()
    {
        
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

    void ChangeSort(int index)
    {
        switch (index)
        {
            case 0:
                ShowStore(store.SortItems("name"));
                break;
            case 1:
                ShowStore(store.SortItems("price"));
                break;
            case 2:
                ShowStore(store.SortItems("id"));
                break;
            case 3:
                ShowStore(store.SortItems("rarity"));
                break;
            case 4:
                ShowStore(store.SortItems("type"));
                break;
        }
    }
}
