using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RuneSelect : MonoBehaviour
{
    [SerializeField] private UI_RuneSelectSlot option1;
    [SerializeField] private UI_RuneSelectSlot option2;
    [SerializeField] private UI_RuneSelectSlot option3;

    public void ShowRuneSelect(System.Action onRuneSelected)
    {
        var runes = RuneManager.Instance.GetRandomRunes(3);
        option1.SetRune(runes[0], onRuneSelected);
        option2.SetRune(runes[1], onRuneSelected);
        option3.SetRune(runes[2], onRuneSelected);

        gameObject.SetActive(true);
    }

    public void HideRuneSelect()
    {
        gameObject.SetActive(false);
    }

}
