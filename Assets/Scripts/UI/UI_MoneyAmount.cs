using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_MoneyAmount : MonoBehaviour
{

    private TextMeshProUGUI moneyAmountText;

    void Start()
    {
        moneyAmountText = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        moneyAmountText.text = PlayerManager.instance.money.ToString();
    }
}
