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
        ShowStore(store.SortItems("id")); // Mostrar inicialmente orden por ID
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            ShowStore(store.SortItems("nombre"));

        if (Input.GetKeyDown(KeyCode.P))
            ShowStore(store.SortItems("precio"));

        if (Input.GetKeyDown(KeyCode.I))
            ShowStore(store.SortItems("id"));

        if (Input.GetKeyDown(KeyCode.R))
            ShowStore(store.SortItems("rareza"));

        if (Input.GetKeyDown(KeyCode.T))
            ShowStore(store.SortItems("tipo"));
    }

  
    public void ShowStore(List<Item> itemList)  // Mostrar la lista de items en la UI
    {
        // Limpiar contenido previo
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        // Crear botones para cada item
        foreach (Item item in itemList)
        {
            GameObject newButton = Instantiate(itemButtonPrefab, contentPanel);

            // Actualizar el texto y el icono del botón
            newButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name;
            newButton.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = "$" + item.price.ToString();
            newButton.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            int itemID = item.id;
            newButton.GetComponent<Button>().onClick.AddListener(() => storeManager.BuyItem(itemID));
        }
    }
}
