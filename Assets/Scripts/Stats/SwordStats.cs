using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStats : MonoBehaviour
{
    public string swordName;

    public Stat damage;

    public virtual void DoDamage(EntityStats _entityStats)
    {
        _entityStats.TakeDamage(damage.GetValue());
    }

}
