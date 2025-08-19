using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = "Money: $" + playerInventory.money.ToString();
    }
}
