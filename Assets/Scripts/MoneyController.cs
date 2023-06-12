using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public static int money = 17000;

    private void Start()
    {
        ReloadMoneyText();
    }

    private void OnEnable()
    {
        PutUpForSaleSystem.onSale += AddMoney;
        BuyUsedPartsSystem.onPurchase += MinusMoney;
    }

    private void OnDisable()
    {
        PutUpForSaleSystem.onSale -= AddMoney;
        BuyUsedPartsSystem.onPurchase -= MinusMoney;
    }

    private void MinusMoney(int amount)
    {
        money -= amount;
        ReloadMoneyText();
    }

    private void AddMoney(int amount)
    {
        money += amount;
        ReloadMoneyText();
    }

    private void ReloadMoneyText()
    {
        moneyText.text = "Δενεγ: " + money.ToString();
    }
}
