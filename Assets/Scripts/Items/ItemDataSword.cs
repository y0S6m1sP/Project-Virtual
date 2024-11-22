using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword Data", menuName = "Data/Sword")]
public class ItemDataSword : ItemData
{
    public GameObject swordPrefab;

    public int damage;

    public override string GetDescription()
    {
        return damage + " damage";
    }

}
