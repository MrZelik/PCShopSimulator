using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public static int money = 17000;

    private void Update()
    {
        moneyText.text = "Δενεγ: " + money.ToString();
    }
}
