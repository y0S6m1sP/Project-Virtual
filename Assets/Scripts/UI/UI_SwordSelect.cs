using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SwordSelect : MonoBehaviour
{
    [SerializeField] private UI_SwordSelectSlot option1;
    [SerializeField] private UI_SwordSelectSlot option2;
    [SerializeField] private UI_SwordSelectSlot option3;

    public void ShowSwordSelect(System.Action onSwordSelected)
    {
        var swords = SwordManager.Instance.GetRandomSwords(3);
        option1.SetSword(swords[0], onSwordSelected);
        option2.SetSword(swords[1], onSwordSelected);
        option3.SetSword(swords[2], onSwordSelected);

        gameObject.SetActive(true);
    }

    public void HideSwordSelect()
    {
        gameObject.SetActive(false);
    }

}
